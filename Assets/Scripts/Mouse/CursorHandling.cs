using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandling : MonoBehaviour
{
    [SerializeField] public Transform cursorTransform;
    [SerializeField] public float cursorSpeed = 1;
    private Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cursorTransform.position += new Vector3((transform.position.x - previousPosition.x) * 2*cursorSpeed, (transform.position.y - previousPosition.y) * cursorSpeed);
        previousPosition = transform.position;
    }
}
