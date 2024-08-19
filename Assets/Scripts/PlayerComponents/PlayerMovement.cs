using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;
    private Vector2 _movementInput;
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private float pushingSpeed = 5f;
    private bool _isTouchingOtherObject = false;
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
            _playerAnimator.SetFloat("Horizontal", _movementInput.x);
            _playerAnimator.SetFloat("Vertical", _movementInput.y);
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
        _playerRigidbody.MovePosition(_playerRigidbody.position + _movementInput * (_isTouchingOtherObject ? pushingSpeed : runningSpeed) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isTouchingOtherObject = true;
        _playerAnimator.SetBool("IsPushing", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isTouchingOtherObject = false;
        _playerAnimator.SetBool("IsPushing", false);
    }
}
