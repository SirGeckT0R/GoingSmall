using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandling : MonoBehaviour
{
    [SerializeField] public Transform cursorTransform;
    private Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cursorTransform.position += new Vector3((transform.position.x - previousPosition.x), (transform.position.y - previousPosition.y));
        previousPosition = transform.position;
    }
}
