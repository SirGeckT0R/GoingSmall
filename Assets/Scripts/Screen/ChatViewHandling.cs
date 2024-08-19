using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum E_ALIGNMENT
{
    LEFT,
    RIGHT, 
    TOP, 
    BOTTOM
}

public class ChatViewHandling : MonoBehaviour
{
    [SerializeField] private GameObject messagePrefab;
    void Update()
    {
    }

    public void AddMessage(string text, E_ALIGNMENT alignment, Color color)
    {



        GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }
}
