using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static FileReader;
using static StoryManager;

public class ChatViewHandling : MonoBehaviour
{

    public delegate void ChatFailed();
    public static event ChatFailed OnChatFailed;
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private GameObject contentElement;
    [SerializeField] private float OptionalSeconds = 60f;
    private IEnumerator _currentCoroutine;


    private void OnEnable()
    {
        StoryManager.OnAnswerSuccess += Success;
        StoryManager.OnChatCompleted+= LoadChat;
    }

    private void OnDisable()
    {
        StoryManager.OnAnswerSuccess -= Success;
        StoryManager.OnChatCompleted -= LoadChat;
    }
    void Update()
    { 
    }

    public void AddMessage(string text, TextAlignmentOptions alignment = default, Color color = default)
    {
        GameObject newMessage = Instantiate(messagePrefab, contentElement.transform);
        newMessage.GetComponentInChildren<TextMeshProUGUI>().text = text;
        //newMessage.GetComponentInChildren<TextMeshProUGUI>().alignment = alignment;

        GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    private void LoadChat(Queue<GameText> chatTexts, int amountOfNonOptionalTexts, bool isFinalMessage)
    {
        _currentCoroutine = ShowChatMessages(chatTexts, amountOfNonOptionalTexts, 1f, isFinalMessage, OptionalSeconds);
        StartCoroutine(_currentCoroutine);
    }

    private IEnumerator ShowChatMessages(Queue<GameText> chatTexts, int amountOfNonOptionalTexts, float nonOptionalDelay, bool isFinalMessage,float optionalDelay = 60f)
    {
        while (chatTexts.Count > 0 && (amountOfNonOptionalTexts-- > 0))
        {
            yield return new WaitForSeconds(nonOptionalDelay);
            string fullText = chatTexts.Dequeue().text;
            AddMessage(fullText);
        }

        if (isFinalMessage)
        {
            yield break;
        }

        while (chatTexts.Count > 0)
        {
            yield return new WaitForSeconds(optionalDelay);
            string fullText = chatTexts.Dequeue().text;
            AddMessage(fullText);
        }

        OnChatFailed();
    }

    private void Success()
    {
        StopCoroutine(_currentCoroutine);
    }
}
