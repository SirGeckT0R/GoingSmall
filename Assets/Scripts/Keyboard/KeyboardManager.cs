using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public delegate void EnterPressed(string message);
    public static event EnterPressed OnEnterPressed;

    private string _typedMessage;
    private void OnEnable()
    {
        KeyboardButtonInteraction.OnButtonPressed += ButtonPressed;
    }

    private void OnDisable()
    {
        KeyboardButtonInteraction.OnButtonPressed -= ButtonPressed;
    }

    private void ButtonPressed(KeyCode key)
    {
        if(key == KeyCode.Clear)
        {
            _typedMessage = _typedMessage.Length > 0 ? _typedMessage.Remove(_typedMessage.Length - 1) : _typedMessage;
        }
        else if(key == KeyCode.Return)
        {
            OnEnterPressed(_typedMessage);
        }
        else
        {
            _typedMessage += key == KeyCode.Space ? " " : key.ToString().Replace("Alpha","");
        }
        Debug.Log(_typedMessage);
    }
}
