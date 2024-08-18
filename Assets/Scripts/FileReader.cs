using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    public StoryTextList storyList = new StoryTextList();

    [System.Serializable]
    public class StoryText
    {
        public string text;
    }

    [System.Serializable]
    public class StoryTextList
    {
        public List<StoryText> list;
    }

    void Start()
    {
        storyList = JsonUtility.FromJson<StoryTextList>(file.text);
    }

}
