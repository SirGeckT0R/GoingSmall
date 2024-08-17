using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
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
        else
        {
            _typedMessage += key == KeyCode.Space ? " " : key.ToString();
        }
        Debug.Log(_typedMessage);
    }
}
