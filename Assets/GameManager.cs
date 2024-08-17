using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<string> answers;
    private void Start()
    {
        answers = new List<string>() { "FF", "Fu"};
    }

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
        Debug.Log(message.Equals(answers[0]));
    }
}
