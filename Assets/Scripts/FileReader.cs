using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    [SerializeField] private TextAsset file;
    public TextList GameTextList = new TextList();

    [System.Serializable]
    public class GameText
    {
        public string text;
    }

    [System.Serializable]
    public class TextList
    {
        public List<GameText> storyList;
        public List<GameText> chatList;
    }

    void Start()
    {
        GameTextList = JsonUtility.FromJson<TextList>(file.text);
    }

}
