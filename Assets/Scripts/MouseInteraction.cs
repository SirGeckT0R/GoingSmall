using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MouseInteraction : MonoBehaviour
{
    public bool _isPlayerInside = false;

    [SerializeField] private string _interactionTag = "";

    void Start()
    {
    }
    void Update()
    {
        if (_isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Horray");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isPlayerInside = false;
        }
    }
}
