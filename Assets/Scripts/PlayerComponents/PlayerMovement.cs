using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    private Vector2 _movementInput;
    [SerializeField] private float speed = 10f;
    //private bool IsFacingRight = true;
    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");

        if (_movementInput.magnitude >= 0.01)
        { 
            _playerAnimator.SetFloat("Vertical", _movementInput.y);
            _playerAnimator.SetFloat("Horizontal", _movementInput.x);
        }

        if (Mathf.Abs(_movementInput.y) > Mathf.Abs(_movementInput.x))
        {
            _movementInput.x = 0;
        }
        else
        {
            _movementInput.y = 0;
        }

        _playerAnimator.SetFloat("Speed", _movementInput.magnitude);
    }

    private void FixedUpdate()
    {
        _playerRigidbody.MovePosition(_playerRigidbody.position + _movementInput * speed * Time.deltaTime);
    }

    //private void Flip()
    //{
    //    if(IsFacingRight && _movementInput.x < 0f || !IsFacingRight && _movementInput.x > 0f)
    //    {
    //        IsFacingRight = !IsFacingRight;
    //        Vector3 localScale = transform.localScale;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
    //}
}
