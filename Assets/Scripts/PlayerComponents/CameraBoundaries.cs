using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CameraBoundaries : Boundaries
{

    void Start()
    {
        AreaBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        SpriteSize = GetComponent<SpriteRenderer>().bounds.size / 2;
    }
}
