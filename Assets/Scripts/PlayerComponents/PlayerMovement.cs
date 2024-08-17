using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D PlayerRigidbody;
    private float verticalInput;
    private float horizontalInput;
    private float speed = 10f;
    private bool IsFacingRight = false;
    void Start()
    {
        
    }

    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 fd = new Vector2(horizontalInput * speed, verticalInput * speed);
        PlayerRigidbody.velocity = fd;
    }

    private void Flip()
    {
        if(IsFacingRight && horizontalInput < 0f || !IsFacingRight && horizontalInput > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
