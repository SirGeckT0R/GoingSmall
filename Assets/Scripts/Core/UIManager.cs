using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static FileReader;

public class UIManager : MonoBehaviour
{
    public delegate void StartScreenHidden(TextMeshProUGUI element);
    public static event StartScreenHidden OnGameStarted;

    private IEnumerator _startTextEnumerator;
    private bool CheckStartScreen = true;

    [SerializeField] private ScreenButton CallAppButton;
    [SerializeField] private ScreenButton BrowserAppButton;
    [SerializeField] private GameObject StartScreenUI;
    [SerializeField] private TextMeshProUGUI StartScreenText;
    [SerializeField] private GameObject EndScreenDialogueUI;
    [SerializeField] private GameObject EndScreenFinUI;
    [SerializeField] private TextMeshProUGUI EndScreenText;
    [SerializeField] private GameObject FailScreenUI;
    [SerializeField] private float timeDelay = 16f;
    private float startTimer = 0f;

    private void OnEnable()
    {
        StoryManager.OnStoryLoaded += ShowStartText;
        BrowserExitButton.OnBrowserCompleted += UnlockCallApp;
    }

    private void OnDisable()
    {
        StoryManager.OnStoryLoaded -= ShowStartText;
        BrowserExitButton.OnBrowserCompleted -= UnlockCallApp;
    }

    private void Start()
    {
        StartScreenUI?.SetActive(true);
    }

    private void Update()
    {
        if (startTimer < 2f)
        { 
            startTimer += Time.deltaTime;
        }
        else
        {
            if (CheckStartScreen && _startTextEnumerator != null && Input.anyKey)
            {
                CheckStartScreen = false;
                StopCoroutine(_startTextEnumerator);
                StartCoroutine(HideStartScreen(0));
            }
        }
    }

    private IEnumerator HideStartScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckStartScreen = false;
        StartScreenUI?.SetActive(false);
        OnGameStarted(null);
    }

    private void ShowStartText(Queue<GameText> startTexts, int amountOfNonOptionalTexts)
    {
        StartCoroutine(TypeWriterEffect.TypeWithDelay(startTexts, StartScreenText, 0.01f, 0f));
        _startTextEnumerator = HideStartScreen(timeDelay);
        StartCoroutine(_startTextEnumerator);
    }

    public void ShowFailScreen() { 
        FailScreenUI?.SetActive(true); 
    }

    public void ShowEndScreen(Queue<GameText> startTexts)
    {
        EndScreenDialogueUI?.SetActive(true);
        StartCoroutine(TypeWriterEffect.TypeWithDelay(startTexts, EndScreenText, 0.01f, 2f));
    }

    public void ChangeEndScreenImage()
    {
        EndScreenDialogueUI?.SetActive(false);
        EndScreenFinUI?.SetActive(true);
    }

    public void UnlockCallApp(E_Screen screen)
    {
        CallAppButton.CanExecute = true;
        BrowserAppButton.ScreenToSwitch = E_Screen.BROWSER_END_PAGE;
    }
}
