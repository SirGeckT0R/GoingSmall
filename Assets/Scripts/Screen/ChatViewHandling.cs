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
    [SerializeField] private Sprite OneLineBackground;
    [SerializeField] private Sprite TwoLineBackground;
    private IEnumerator _currentCoroutine;


    private void OnEnable()
    {
        StoryManager.OnAnswerSuccess += Success;
        StoryManager.OnPlayerMessaged += ShowPlayerMessage;
        StoryManager.OnNextChatMessage+= LoadChat;
    }

    private void OnDisable()
    {
        StoryManager.OnAnswerSuccess -= Success;
        StoryManager.OnPlayerMessaged -= ShowPlayerMessage;
        StoryManager.OnNextChatMessage -= LoadChat;
    }
    void Update()
    {
        GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    public void AddMessage(string text, TextAnchor anchor = TextAnchor.MiddleLeft, Color color = default)
    {
        GameObject newMessage = Instantiate(messagePrefab, contentElement.transform);
        //newMessage.GetComponentInChildren<SpriteRenderer>().sprite = text.Length > 19 ? TwoLineBackground: OneLineBackground;
        newMessage.GetComponentInChildren<TextMeshProUGUI>().text = text;
        newMessage.GetComponent<HorizontalLayoutGroup>().childAlignment = anchor;

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

    private void ShowPlayerMessage(string playerText, TextAnchor anchor = TextAnchor.MiddleRight, Color color = default)
    {
        AddMessage(playerText, anchor, color);
    }

    private void Success()
    {
        StopCoroutine(_currentCoroutine);
    }
}
