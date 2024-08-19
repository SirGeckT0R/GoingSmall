using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Device;

public enum E_Screen
{
    DESKTOP,
    CHAT,
    CALL_APP,
    WEB_BROWSER,
    BROWSER_TITLE_PAGE,
    BROWSER_SEATS_PAGE,
    BROWSER_CART_PAGE,
    BROWSER_AD_PAGE,
    BROWSER_END_PAGE,
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private string tagOfScreens;
    private List<GameObject> _collectionOfScreens = new List<GameObject>();
    private GameObject _currentScreen;
    private void OnEnable()
    {
        ScreenButton.OnScreenButtonPressed += SwitchScreen;
        SeatsPageScreenButton.OnSeatsButtonPressed += SwitchScreen;
    }

    private void OnDisable()
    {
        ScreenButton.OnScreenButtonPressed -= SwitchScreen;
        SeatsPageScreenButton.OnSeatsButtonPressed -= SwitchScreen;
    }
    void Start()
    {
        Component[] children = System.Array.FindAll(transform.GetComponentsInChildren(typeof(Transform), true),
            (child) => child.tag.Equals(tagOfScreens) && child.transform != transform
            );

        foreach(Component child in children)
        {
            _collectionOfScreens.Add(child.gameObject);
            if(child.gameObject.name.ToLower().Replace("_", "").Contains("web"))
            {
                continue;
            }
            child.gameObject.SetActive(false);
        }
        
        _currentScreen = _collectionOfScreens.Find((screen)=>screen.name.ToLower().Replace("_","").Contains("chat"));
        _currentScreen?.SetActive(true);
    }

    private void SwitchScreen(E_Screen screenToSwitchTo)
    {
        string screenName = screenToSwitchTo.ToString().ToLower().Replace("_", "");
        _currentScreen?.SetActive(false);
        _currentScreen = _collectionOfScreens.Find(
            (screen) =>screen.name.ToLower().Contains(screenName)
            );
        _currentScreen?.SetActive(true);
    }
}
