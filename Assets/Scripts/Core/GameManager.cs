using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> answers = new List<string>();

    private void OnEnable()
    {
        KeyboardManager.OnEnterPressed += CheckMessage;        
    }

    private void OnDisable()
    {
        KeyboardManager.OnEnterPressed -= CheckMessage;
    }

    private void CheckMessage(string message)
    {
        message = Regex.Replace(message.Trim(), @"\s+", " ");
        Debug.Log(message.Equals(answers[0]));
    }
}
