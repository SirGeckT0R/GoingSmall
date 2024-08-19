using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static KeyboardManager;

public class KeyboardManager : MonoBehaviour
{
    public delegate void EnterPressed(string message);
    public static event EnterPressed OnEnterPressed;
    public delegate void MessageChanged(string message, GameEndButton gameEndButton);
    public static event MessageChanged OnMessageChanged;
    [SerializeField] private TextMeshProUGUI InputStringChatUI;
    [SerializeField] private TextMeshProUGUI InputStringCallUI;
    [SerializeField] private GameEndButton endButton;
    private bool isChatWriting = true;

    private string _typedMessage;
    private void OnEnable()
    {
        KeyboardButtonInteraction.OnButtonPressed += ButtonPressed;
        StoryManager.OnAnswerSuccess += ClearMessage;
        StoryManager.OnChatCompleted += SwitchBehaviour;
    }

    private void OnDisable()
    {
        KeyboardButtonInteraction.OnButtonPressed -= ButtonPressed;
        StoryManager.OnAnswerSuccess -= ClearMessage;
        StoryManager.OnChatCompleted -= SwitchBehaviour;
    }

    private void ButtonPressed(KeyCode key)
    {
        if (isChatWriting)
        {
            if (key == KeyCode.Clear)
            {
                _typedMessage = _typedMessage.Length > 0 ? _typedMessage.Remove(_typedMessage.Length - 1) : _typedMessage;
            }
            else if (key == KeyCode.Return)
            {
                OnEnterPressed(_typedMessage.ToLower());
            }
            else
            {
                _typedMessage += key == KeyCode.Space ? " " : key.ToString().ToLower().Replace("alpha", "");
            }
            InputStringChatUI.text = _typedMessage + "|";
            Debug.Log(_typedMessage);
            return;
        }   

        if (key == KeyCode.Clear)
        {
            _typedMessage = _typedMessage.Length > 0 ? _typedMessage.Remove(_typedMessage.Length - 1) : _typedMessage;
        }
        else if (key != KeyCode.Return)
        {
            _typedMessage += key == KeyCode.Space ? " " : key.ToString().ToLower().Replace("alpha", "");
        }

        OnMessageChanged(_typedMessage, endButton);
        InputStringCallUI.text = _typedMessage + "|";
        Debug.Log(_typedMessage);
    }

    private void ClearMessage()
    {
        _typedMessage = "";
        InputStringChatUI.text = _typedMessage;
    }

    private void SwitchBehaviour()
    {
        _typedMessage = "";
        isChatWriting = false;
    }
}
