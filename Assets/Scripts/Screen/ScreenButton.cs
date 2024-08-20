using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenButton : MonoBehaviour
{
    public delegate void ScreenButtonPressed(E_Screen screen);
    public static event ScreenButtonPressed OnScreenButtonPressed;

    public bool CanExecute = false;
    [SerializeField] public bool ShouldBeBlocked = false;

    [SerializeField] public E_Screen ScreenToSwitch;

    private void OnEnable()
    {
        StoryManager.OnChatCompleted += () => CanExecute = true;
        //StoryManager.OnBrowserCompleted += () => CanExecute = true;
    }

    private void OnDisable()
    {
        StoryManager.OnChatCompleted -= () => CanExecute = true;
        //StoryManager.OnBrowserCompleted -= () => CanExecute = true;
    }
    public virtual void Execute()
    {
        if (ShouldBeBlocked && !CanExecute)
        {
            return;
        }

        Debug.Log("Left mouse on screen clicked");
        OnScreenButtonPressed(ScreenToSwitch);
    }

    
}
