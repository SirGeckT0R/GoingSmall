using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseInteraction : ZoneInteraction
{
    private CursorInteraction _cursor;
    [SerializeField] private GameObject PopUp;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isObjectInside = true;
            if (PopUp != null)
            {
                PopUp.SetActive(true);
            }
            Execute();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isObjectInside = false;
            if (PopUp != null)
            {
                PopUp.SetActive(false);
            }
        }
    }
}
