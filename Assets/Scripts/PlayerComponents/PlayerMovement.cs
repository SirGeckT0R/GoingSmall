using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody;
    private float _verticalInput;
    private float _horizontalInput;
    [SerializeField] private float speed = 10f;
    private bool IsFacingRight = true;
    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(_verticalInput) > Mathf.Abs(_horizontalInput))
        {
            _horizontalInput = 0;
        }
        else
        {
            _verticalInput = 0;
        }

        Vector2 movementVector = new Vector2(_horizontalInput * speed, _verticalInput * speed);
        _playerRigidbody.velocity = movementVector;
        Flip();
    }

    private void Flip()
    {
        if(IsFacingRight && _horizontalInput < 0f || !IsFacingRight && _horizontalInput > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
