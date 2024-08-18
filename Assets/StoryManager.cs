using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using static FileReader;


[RequireComponent(typeof(FileReader))]
public class StoryManager : MonoBehaviour
{
    public delegate void StoryLoaded(GameText startText);
    public static event StoryLoaded OnStoryLoaded;

    [SerializeField] private TextMeshProUGUI textElement;
    public List<GameText> storyList;


    private void OnEnable()
    {
        UIManager.OnGameStarted += ShowStoryText;
    }

    private void OnDisable()
    {
        UIManager.OnGameStarted -= ShowStoryText;
    }
    private void Update()
    {
        if (storyList.Count == 0)
        {
            storyList = GetComponent<FileReader>().GameTextList.storyList;
            if(storyList != null)
            {
                OnStoryLoaded(storyList[0]);
            }
            return;
        }

    }

    private void ShowStoryText()
    { 
        StartCoroutine(TypeWriterEffect.ShowInSucession(new Queue<FileReader.GameText>(storyList.GetRange(1, 5)), textElement, 0.05f));
    }
}
