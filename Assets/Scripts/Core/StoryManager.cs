using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FileReader;


[RequireComponent(typeof(FileReader))]
public class StoryManager : MonoBehaviour
{
    public delegate void ChatCompleted();
    public static event ChatCompleted OnChatCompleted;
    public delegate void AnswerSuccess();
    public static event AnswerSuccess OnAnswerSuccess;
    public delegate void PlayerMessage(string playerText, TextAnchor anchor = TextAnchor.MiddleRight, Color color = default);
    public static event PlayerMessage OnPlayerMessaged;
    public delegate void StoryLoaded(Queue<GameText> startTexts,int amountOfNonOptionalTexts);
    public static event StoryLoaded OnStoryLoaded;
    public delegate void NextChatMessage(Queue<GameText> startTexts, int amountOfNonOptionalTexts, bool finalMessage);
    public static event NextChatMessage OnNextChatMessage;

    [SerializeField] private TextMeshProUGUI textElement;
    public List<GameText> storyList = new List<GameText>();
    public List<GameText> chatList=new List<GameText>();
    //public delegate void ChatComplete();
    //public static event ChatComplete OnChatCompleted;
    [SerializeField] private List<string> answers = new List<string>();
    private List<int> AmountOfMistakes = new List<int>() { 1, 2, 3, 0};
    private List<(int, int)> AmountOfChatMessages= new List<(int, int)>() { ( 3 ,1),( 4,2),(4,2),(1,1)};
    private List<int> AmountOfStoryTexts= new List<int>() {1, 6, 1,1,3,2, 1};

    private bool isFinalMessage = false;
    private bool isChatStarted = false;

    private void OnEnable()
    {
        KeyboardManager.OnEnterPressed += CheckMessage;
        KeyboardManager.OnMessageChanged += CheckCallAppMessage;
        ChatViewHandling.OnChatFailed += Fail;
        UIManager.OnGameStarted += ShowStoryText;
        BrowserExitButton.OnBrowserCompleted += BrowserSuccess;
        GameEndButton.OnGameEnd += GameEnd;
    }

    private void OnDisable()
    {
        KeyboardManager.OnEnterPressed -= CheckMessage;
        KeyboardManager.OnMessageChanged -= CheckCallAppMessage;
        ChatViewHandling.OnChatFailed -= Fail;
        UIManager.OnGameStarted -= ShowStoryText;
        BrowserExitButton.OnBrowserCompleted -= BrowserSuccess;
        GameEndButton.OnGameEnd -= GameEnd;
    }
    private void Update()
    {
        if (storyList.Count == 0)
        {
            storyList = GetComponent<FileReader>().GameTextList.storyList;
            
            if(storyList.Count != 0)
            {
                OnStoryLoaded(new Queue<FileReader.GameText>(storyList.GetRange(0, AmountOfStoryTexts[0])), 1);
                storyList.RemoveRange(0, AmountOfStoryTexts[0]);
                AmountOfStoryTexts.RemoveAt(0);
            }
            return;
        }
        if (chatList.Count == 0)
        {
            chatList = GetComponent<FileReader>().GameTextList.chatList;
            return;
        }

    }

    private void ShowStoryText(TextMeshProUGUI element = null)
    {
        if (!isChatStarted)
        {
            List<FileReader.GameText> listToSend = chatList.GetRange(1, AmountOfChatMessages[0].Item1);
            listToSend.Add(chatList[0]);
            chatList.RemoveRange(1, AmountOfChatMessages[0].Item1);
            OnNextChatMessage(new Queue<FileReader.GameText>(listToSend), AmountOfChatMessages[0].Item2, isFinalMessage);
            AmountOfChatMessages.RemoveAt(0);
            isChatStarted = true;
        }
        if(element == null)
        {
            element = textElement;
        }
        StartCoroutine(TypeWriterEffect.TypeWithDelay(new Queue<FileReader.GameText>(storyList.GetRange(0, AmountOfStoryTexts[0])), element, 0.05f));
        storyList.RemoveRange(0, AmountOfStoryTexts[0]);
        AmountOfStoryTexts.RemoveAt(0);
    }
    private void CheckMessage(string message)
    {
        message = Regex.Replace(message.ToLower().Trim(), @"\s+", " ");
        if (ValidateMessage(message))
        {
            OnAnswerSuccess();
            OnPlayerMessaged(message);
            ShowStoryText(textElement);
            answers.RemoveAt(0);
            AmountOfMistakes.RemoveAt(0);
            if (answers.Count == 1)
            {
                OnChatCompleted();
                isFinalMessage = true;
            }

            List<FileReader.GameText> listToSend = chatList.GetRange(1, AmountOfChatMessages[0].Item1);
            listToSend.Add(chatList[0]);
            chatList.RemoveRange(1, AmountOfChatMessages[0].Item1);
            OnNextChatMessage(new Queue<FileReader.GameText>(listToSend), AmountOfChatMessages[0].Item2, isFinalMessage);
            AmountOfChatMessages.RemoveAt(0);
        }
    }

    private bool ValidateMessage(string message)
    {
        if(answers.Count == 1)
        {
            return answers[0].Equals(Regex.Replace(message, @"\s+", ""));
        }
        if (answers.Count == 2 && !message.Contains("9"))
        {
            return false;
        }
        if (answers.Count > 1)
        {
            int currentAmountOfMistakes = 0;
            if (Mathf.Abs(answers[0].Length - message.Length) > AmountOfMistakes[0])
            {
                return false;
            }
            string ShortestString = answers[0].Length > message.Length ? message : answers[0];
            for (int i = 0; i < ShortestString.Length; i++)
            {
                currentAmountOfMistakes += message[i] == answers[0][i] ? 0 : 1;
            }

            if (currentAmountOfMistakes < AmountOfMistakes[0])
            {
                return true;
            }

            Debug.Log(message.Equals(answers[0]));
            return false;
        }
        return false;
    }

    private void Fail()
    {
        StartCoroutine(ShowFailScreen(2f));
    }

    private IEnumerator ShowFailScreen(float screenDelay)
    {
        yield return new WaitForSeconds(screenDelay);
        GetComponent<UIManager>().ShowFailScreen();

        yield return new WaitForSeconds(5f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void BrowserSuccess(E_Screen screen)
    {
        ShowStoryText(textElement);
    }

    private void CheckCallAppMessage(string message, GameEndButton gameEndButton)
    {
        message = Regex.Replace(message.ToLower().Trim(), @"\s+", " ");
        if (ValidateMessage(message))
        {
            answers.RemoveAt(0);
            AmountOfMistakes.RemoveAt(0);

            gameEndButton.CanExecute = true;
            return;
        }

        gameEndButton.CanExecute = false;
    }

    private void GameEnd(E_Screen screen)
    {
        StartCoroutine(ShowGameEnd(3f));
    }

    private IEnumerator ShowGameEnd(float screenDelay)
    {
        yield return new WaitForSeconds(screenDelay);
        GetComponent<UIManager>().ShowEndScreen(new Queue<FileReader.GameText>(storyList.GetRange(0, AmountOfStoryTexts[0])));

        storyList.RemoveRange(0, AmountOfStoryTexts[0]);
        AmountOfStoryTexts.RemoveAt(0);

        yield return new WaitForSeconds(30f);
        GetComponent<UIManager>().ChangeEndScreenImage();
        yield return new WaitForSeconds(10f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
