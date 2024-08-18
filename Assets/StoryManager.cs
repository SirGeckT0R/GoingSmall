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
    [SerializeField] private TextMeshProUGUI textElement;
    private FileReader.StoryTextList _storyList;

    private bool isFirst = true;
    private float timer = 0f;
    private float messageCooldown = 3f;

    void Start()
    {

    }

    private void Update()
    { 
        if (_storyList == null || _storyList.list == null)
        {
            _storyList = GetComponent<FileReader>().storyList;
            return;
        }

        if (isFirst)
        {
            StartCoroutine(ShowInSucession(new Queue<FileReader.StoryText>(_storyList.list.GetRange(0, 2))));
            isFirst = false;
        }
    }

    private IEnumerator ShowInSucession(Queue<FileReader.StoryText> storyTexts)
    {
        while (storyTexts.Count > 0)
        {
            yield return new WaitForSeconds(2f);
            textElement.text = storyTexts.Dequeue().text;
            Debug.Log(textElement.text);

        }
    }
}
