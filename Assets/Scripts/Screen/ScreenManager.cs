using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Screen
{
    DESKTOP,
    CHAT,
    SKYPE,
    WEB_BROWSER,
}

public class ScreenManager : MonoBehaviour
{
    private List<GameObject> _collectionOfScreens = new List<GameObject>();
    private GameObject _currentScreen;
    private void OnEnable()
    {
        ScreenButton.OnScreenButtonPressed += SwitchScreen;
    }

    private void OnDisable()
    {
        ScreenButton.OnScreenButtonPressed -= SwitchScreen;
    }
    void Start()
    {
        int children = transform.childCount;
        for(int i = 0; i < children; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.name.Contains("Screen"))
            {
                _collectionOfScreens.Add(child);
                child.SetActive(false);
            }
        }

        _currentScreen = _collectionOfScreens.Find((screen)=>screen.name.ToLower().Contains("desktop"));
        _currentScreen?.SetActive(true);
    }

    private void SwitchScreen(E_Screen screenToSwitchTo)
    {
        _currentScreen?.SetActive(false);
        _currentScreen = _collectionOfScreens.Find(
            (screen) =>screen.name.ToLower().Contains(screenToSwitchTo.ToString().ToLower())
            );
        _currentScreen?.SetActive(true);
    }
}
