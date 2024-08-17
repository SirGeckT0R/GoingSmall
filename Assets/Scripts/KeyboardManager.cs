using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    private string _typedMessage;
    private void OnEnable()
    {
        KeyboardInteraction.OnButtonPressed += ButtonPressed;
    }

    private void OnDisable()
    {
        KeyboardInteraction.OnButtonPressed -= ButtonPressed;
    }

    private void ButtonPressed(KeyCode key)
    {
        _typedMessage += key == KeyCode.Space ? " " : key.ToString();
        Debug.Log(_typedMessage);
    }
}
