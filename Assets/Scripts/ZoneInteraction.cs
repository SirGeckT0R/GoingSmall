using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ZoneInteraction : MonoBehaviour
{   
    protected bool _isObjectInside = false;

    [SerializeField] protected string _interactionTag = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isObjectInside = true;
            Execute();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactionTag.Equals(collision.gameObject.tag))
        {
            _isObjectInside = false;
        }
    }

    protected virtual void Execute()
    {
    }
}
