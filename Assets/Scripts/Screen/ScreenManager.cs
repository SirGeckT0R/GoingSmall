using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Device;

public enum E_Screen
{
    DESKTOP,
    CHAT,
    WEB_BROWSER,
    BROWSER_TITLE_PAGE,
    BROWSER_SEATS_PAGE,
    BROWSER_CART_PAGE,
    BROWSER_END_PAGE,
    CALL_APP_TITLE,
    CALL_APP_CALL,
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private string tagOfScreens;
    private List<GameObject> _collectionOfScreens = new List<GameObject>();
    private GameObject _currentScreen;

    [SerializeField] private GameObject adScreen;
    [SerializeField] private ScreenButton browserButton;
    [SerializeField] private float adCooldown;
    private float _adTimer;
    private bool isOnBrowserScreen;
    private void OnEnable()
    {
        ScreenButton.OnScreenButtonPressed += SwitchScreen;
        SeatsPageScreenButton.OnSeatsButtonPressed += SwitchScreen;
        BrowserExitButton.OnBrowserCompleted += SwitchScreen;
    }

    private void OnDisable()
    {
        ScreenButton.OnScreenButtonPressed -= SwitchScreen;
        SeatsPageScreenButton.OnSeatsButtonPressed -= SwitchScreen;
        BrowserExitButton.OnBrowserCompleted -= SwitchScreen;
    }
    void Start()
    {
        Component[] children = System.Array.FindAll(transform.GetComponentsInChildren(typeof(Transform), true),
            (child) => child.tag.Equals(tagOfScreens) && child.transform != transform
            );

        foreach(Component child in children)
        {
            _collectionOfScreens.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        
        _currentScreen = _collectionOfScreens.Find((screen)=>screen.name.ToLower().Replace("_","").Contains("chat"));
        _currentScreen?.SetActive(true);
    }

    private void Update()
    {
        if (isOnBrowserScreen)
        {
            _adTimer += Time.deltaTime;
            if (_adTimer > adCooldown)
            {
                _adTimer = 0;
                adScreen.SetActive(true);
            }
        }
    }

    private void SwitchScreen(E_Screen screenToSwitchTo)
    {
        string screenName = screenToSwitchTo.ToString().ToLower().Replace("_", "");
        _currentScreen?.SetActive(false);
        _currentScreen = _collectionOfScreens.Find(
            (screen) =>screen.name.ToLower().Contains(screenName)
            );
        if (_currentScreen.name.ToLower().Contains("browsertitle"))
        {
            isOnBrowserScreen = true;
        }
        _currentScreen?.SetActive(true);
    }

    private void BlockBrowser(E_Screen screenToSwitchTo)
    {
        isOnBrowserScreen = false;
        browserButton.ShouldBeBlocked = true;
        SwitchScreen(screenToSwitchTo);
    }
}
