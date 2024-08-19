using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using static FileReader;


[RequireComponent(typeof(FileReader))]
public class StoryManager : MonoBehaviour
{
    public delegate void AnswerSuccess();
    public static event AnswerSuccess OnAnswerSuccess;
    public delegate void StoryLoaded(Queue<GameText> startTexts,int amountOfNonOptionalTexts);
    public static event StoryLoaded OnStoryLoaded;
    public delegate void ChatCompleted(Queue<GameText> startTexts, int amountOfNonOptionalTexts, bool finalMessage);
    public static event ChatCompleted OnChatCompleted;

    [SerializeField] private TextMeshProUGUI textElement;
    public List<GameText> storyList = new List<GameText>();
    public List<GameText> chatList=new List<GameText>();
    //public delegate void ChatComplete();
    //public static event ChatComplete OnChatCompleted;
    [SerializeField] private List<string> answers = new List<string>();
    [SerializeField] private List<(int, int)> AmountOfChatMessages= new List<(int, int)>() { ( 3 ,1),( 4,2),(4,2),(1,1)};

    private bool isFinalMessage = false;



    private void OnEnable()
    {
        KeyboardManager.OnEnterPressed += CheckMessage;
        ChatViewHandling.OnChatFailed += Fail;
        UIManager.OnGameStarted += ShowStoryText;
    }

    private void OnDisable()
    {
        KeyboardManager.OnEnterPressed -= CheckMessage;
        ChatViewHandling.OnChatFailed -= Fail;
        UIManager.OnGameStarted -= ShowStoryText;
    }
    private void Update()
    {
        if (storyList.Count == 0)
        {
            storyList = GetComponent<FileReader>().GameTextList.storyList;
            
            if(storyList.Count != 0)
            {
                OnStoryLoaded(new Queue<FileReader.GameText>(storyList.GetRange(0,1)), 1);
            }
            return;
        }
        if (chatList.Count == 0)
        {
            chatList = GetComponent<FileReader>().GameTextList.chatList;
            if (chatList.Count != 0)
            {
                List<FileReader.GameText> listToSend = chatList.GetRange(1, AmountOfChatMessages[0].Item1);
                listToSend.Add(chatList[0]);
                chatList.RemoveRange(1, AmountOfChatMessages[0].Item1);
                OnChatCompleted(new Queue<FileReader.GameText>(listToSend), AmountOfChatMessages[0].Item2, isFinalMessage); 
                AmountOfChatMessages.RemoveAt(0);
            }
            return;
        }

    }

    private void ShowStoryText()
    { 
        StartCoroutine(TypeWriterEffect.TypeWithDelay(new Queue<FileReader.GameText>(storyList.GetRange(1, 5)), textElement, 0.05f));
    }
    private void CheckMessage(string message)
    {
        message = Regex.Replace(message.Trim(), @"\s+", " ");
        if (ValidateMessage(message))
        {
            OnAnswerSuccess();
            answers.RemoveAt(0);
            if (answers.Count == 0)
            {
                isFinalMessage = true;
            }
            List<FileReader.GameText> listToSend = chatList.GetRange(1, AmountOfChatMessages[0].Item1);
            listToSend.Add(chatList[0]);
            chatList.RemoveRange(1, AmountOfChatMessages[0].Item1);
            OnChatCompleted(new Queue<FileReader.GameText>(listToSend), AmountOfChatMessages[0].Item2, isFinalMessage);
            AmountOfChatMessages.RemoveAt(0);
        }
    }

    private bool ValidateMessage(string message)
    {
        if(answers.Count > 0)
        {
            Debug.Log(message.Equals(answers[0]));
            return message.Equals(answers[0]);
        }
        return false;
    }

    private void Fail()
    {
        Debug.Log("Fail");
    }
}
