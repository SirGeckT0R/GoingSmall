using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserExitButton : ScreenButton
{
    public delegate void BrowserCompleted(E_Screen screen);
    public static event BrowserCompleted OnBrowserCompleted;
    public delegate void ScreenButtonPressed(E_Screen screen);
    public static event ScreenButtonPressed OnScreenButtonPressed;

    public bool isCompleted = false;
    public override void Execute()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            OnBrowserCompleted(ScreenToSwitch);
        }
        else
        {
            OnScreenButtonPressed(ScreenToSwitch);
        }
    }
}
