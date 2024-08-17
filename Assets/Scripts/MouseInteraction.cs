using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseInteraction : ZoneInteraction
{

    void Update()
    {
        if (_isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Left mouse clicked");
        }
    }   
}
