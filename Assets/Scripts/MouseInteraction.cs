using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseInteraction : ZoneInteraction
{
    private CursorInteraction _cursor;
    private void Start()
    {
        _cursor = transform.parent.GetComponent<CursorHandling>().cursorTransform.GetComponent<CursorInteraction>();
    }
    void Update()
    {
        if (_isObjectInside && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Left mouse clicked");
            _cursor.Interact();
        }
    }   
}
