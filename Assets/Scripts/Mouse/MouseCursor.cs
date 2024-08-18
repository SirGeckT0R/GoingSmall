using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName ="MouseCursor")]
public class MouseCursor : ScriptableObject
{
    [SerializeField] public Transform mouseCursor;
}
