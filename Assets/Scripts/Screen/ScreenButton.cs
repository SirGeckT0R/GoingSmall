using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenButton : MonoBehaviour
{
    public delegate void ScreenButtonPressed(E_Screen screen);
    public static event ScreenButtonPressed OnScreenButtonPressed;

    [SerializeField] private E_Screen ScreenToSwitch;
    public void Execute()
    {
       Debug.Log("Left mouse on screen clicked");
       OnScreenButtonPressed(ScreenToSwitch);
    }
}
