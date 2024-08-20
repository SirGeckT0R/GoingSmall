using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Device;
using static BrowserExitButton;

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

    public delegate void BrowserFail();
    public static event BrowserFail OnBrowserFailed;

    [SerializeField] private string tagOfScreens;
    private List<GameObject> _collectionOfScreens = new List<GameObject>();
    private GameObject _currentScreen;

    [SerializeField] private GameObject adScreen;
    [SerializeField] private ScreenButton browserButton;
    [SerializeField] private float adCooldown;
    [SerializeField] private float failCooldown = 180f;
    private float _adTimer;
    private float _failTimer;
    private bool isOnBrowserScreen = false;
    private bool isBrowserCompleted = false;
    private void OnEnable()
    {
        ScreenButton.OnScreenButtonPressed += SwitchScreen;
        SeatsPageScreenButton.OnSeatsButtonPressed += SwitchScreen;
        BrowserExitButton.OnBrowserCompleted += CompleteBrowser;
        GameEndButton.OnGameEnd += SwitchScreen;
    }

    private void OnDisable()
    {
        ScreenButton.OnScreenButtonPressed -= SwitchScreen;
        SeatsPageScreenButton.OnSeatsButtonPressed -= SwitchScreen;
        BrowserExitButton.OnBrowserCompleted -= CompleteBrowser;
        GameEndButton.OnGameEnd -= SwitchScreen;
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
            _failTimer += Time.deltaTime;
            if(_failTimer > failCooldown && !isBrowserCompleted)
            {
                OnBrowserFailed();
                _failTimer = 0;
            }
            if (_adTimer > adCooldown)
            {
                _adTimer = 0;
                adScreen.SetActive(true);
            }
            return;
        }
        _failTimer = 0f;
    }

    private void SwitchScreen(E_Screen screenToSwitchTo)
    {
        string screenName = screenToSwitchTo.ToString().ToLower().Replace("_", "");
        _currentScreen?.SetActive(false);
        _currentScreen = _collectionOfScreens.Find(
            (screen) =>screen.name.ToLower().Contains(screenName)
            );
        if (_currentScreen.name.ToLower().Contains("browser"))
        {
            isOnBrowserScreen = true;
        }
        _currentScreen?.SetActive(true);
    }

    private void CompleteBrowser(E_Screen screenToSwitchTo)
    {
        isBrowserCompleted = true;
        isOnBrowserScreen = false;
        SwitchScreen(screenToSwitchTo);
    }
}
