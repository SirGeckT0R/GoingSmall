using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static FileReader;

public class UIManager : MonoBehaviour
{
    public delegate void StartScreenHidden(TextMeshProUGUI element);
    public static event StartScreenHidden OnGameStarted;

    private List<GameText> _storyList;

    [SerializeField] private GameObject StartScreenUI;
    [SerializeField] private TextMeshProUGUI StartScreenText;
    [SerializeField] private GameObject EndScreenUI;
    [SerializeField] private float timeDelay = 8f;

    private void OnEnable()
    {
        StoryManager.OnStoryLoaded += ShowStartText;
    }

    private void OnDisable()
    {
        StoryManager.OnStoryLoaded -= ShowStartText;
    }

    private void Start()
    {
        StartScreenUI?.SetActive(true);
    }

    private IEnumerator HideStartScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartScreenUI?.SetActive(false);
        OnGameStarted(null);
    }

    private void ShowStartText(Queue<GameText> startTexts, int amountOfNonOptionalTexts)
    {
        StartCoroutine(TypeWriterEffect.TypeWithDelay(startTexts, StartScreenText, 0.01f, 0f));
        StartCoroutine(HideStartScreen(timeDelay));
    }
}
