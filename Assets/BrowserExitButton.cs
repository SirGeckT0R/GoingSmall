using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserExitButton : ScreenButton
{
    public delegate void BrowserCompleted(E_Screen screen);
    public static event BrowserCompleted OnBrowserCompleted;
    public override void Execute()
    {
        OnBrowserCompleted(ScreenToSwitch);
    }
}
