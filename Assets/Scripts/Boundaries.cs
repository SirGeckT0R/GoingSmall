using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Boundaries : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;
    protected Vector2 AreaBounds;
    protected Vector2 SpriteSize;

    void Start()
    {
        AreaBounds = collider.bounds.size / 2;
        SpriteSize = GetComponent<SpriteRenderer>().bounds.size / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, (AreaBounds.x * -1) + SpriteSize.x, AreaBounds.x - SpriteSize.x);
        viewPos.y = Mathf.Clamp(viewPos.y, (AreaBounds.y * -1) + SpriteSize.y, AreaBounds.y - SpriteSize.y);
        transform.position = viewPos;
    }
}
