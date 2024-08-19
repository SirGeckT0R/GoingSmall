using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public delegate void EnterPressed(string message);
    public static event EnterPressed OnEnterPressed;
    [SerializeField] private TextMeshProUGUI InputStringUI;

    private string _typedMessage;
    private void OnEnable()
    {
        KeyboardButtonInteraction.OnButtonPressed += ButtonPressed;
        StoryManager.OnAnswerSuccess += ClearMessage;
    }

    private void OnDisable()
    {
        KeyboardButtonInteraction.OnButtonPressed -= ButtonPressed;
        StoryManager.OnAnswerSuccess -= ClearMessage;
    }

    private void ButtonPressed(KeyCode key)
    {
        if(key == KeyCode.Clear)
        {
            _typedMessage = _typedMessage.Length > 0 ? _typedMessage.Remove(_typedMessage.Length - 1) : _typedMessage;
        }
        else if(key == KeyCode.Return)
        {
            OnEnterPressed(_typedMessage.ToLower());
        }
        else
        {
            _typedMessage += key == KeyCode.Space ? " " : key.ToString().ToLower().Replace("alpha","");
        }
        InputStringUI.text = _typedMessage + "|";
        Debug.Log(_typedMessage);
    }

    private void ClearMessage()
    {
        _typedMessage = "";
        InputStringUI.text = _typedMessage;
    }
}
