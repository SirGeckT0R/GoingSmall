using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Boundaries : MonoBehaviour
{
    [SerializeField] protected Transform Area;
    protected Vector2 AreaBounds;
    protected Vector2 SpriteSize;

    void Start()
    {
        AreaBounds = Area.GetComponent<Collider2D>().bounds.size / 2;
        SpriteSize = GetComponent<SpriteRenderer>().bounds.size / 2;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x,  
            (Area.position.x - AreaBounds.x) + SpriteSize.x, 
            (Area.position.x + AreaBounds.x) - SpriteSize.x);

        viewPos.y = Mathf.Clamp(viewPos.y, 
            (Area.position.y - AreaBounds.y) + SpriteSize.y, 
            (Area.position.y + AreaBounds.y) - SpriteSize.y);

        transform.position = viewPos;
    }
}
