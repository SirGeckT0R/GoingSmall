using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class KeyboardButtonSpawn : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private float scaleOfButtons = 1f;
    [SerializeField] private float paddingVertical = 0.5f;
    [SerializeField] private float paddingHorizontal = 0.5f;
    [SerializeField] private KeyCode startKey = KeyCode.A;
    [SerializeField] private KeyCode endKey = KeyCode.Z;

    private Vector2 _sizeOfKeyboard = Vector2.zero;
    private Vector2 _sizeOfButton = Vector2.zero;

    private void Start()
    {
        _sizeOfButton = ButtonPrefab.GetComponent<SpriteRenderer>().bounds.size * scaleOfButtons;
        _sizeOfKeyboard = GetComponent<BoxCollider2D>().bounds.size;
        float rowWidth = 0f;
        int cols = 0;
        do
        {
            rowWidth += 2 * _sizeOfButton.x + paddingHorizontal;
            cols++;
        } while (rowWidth < _sizeOfKeyboard.x);

        float posX = 0f;
        float posY = 0f;
        int amountOfKeys = endKey - startKey + 1;
        int currentRow = 0;
        int currentColumn = 0;
        Vector3 posOwner = transform.position + new Vector3(-_sizeOfKeyboard.x / 2, _sizeOfKeyboard.y / 2, 0);
        for (int i = 0; i < amountOfKeys; i++)
        {
            GameObject keyButton = (GameObject)PrefabUtility.InstantiatePrefab(ButtonPrefab, transform);
            if (currentColumn >= cols)
            {
                currentRow++;
                currentColumn = 0;
                posY = currentRow * (2 * _sizeOfButton.y + paddingVertical);
            }

            posX = currentColumn * (2 * _sizeOfButton.x + paddingHorizontal);
            keyButton.transform.localScale = new Vector3(scaleOfButtons, scaleOfButtons, scaleOfButtons);
            keyButton.transform.position = new Vector3(posOwner.x + posX , posOwner.y - posY);
            currentColumn++;
        }
    }
}
