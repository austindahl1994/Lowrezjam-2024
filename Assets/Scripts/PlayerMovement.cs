using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform GroundCheck;
    public LayerMask GroundLayerMask;

    [SerializeField]
    private float _playerSpeed;

    private float _clickTime, _duration;
    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private int _playerHP = 100;


    private Vector2 _playerMoveInput;
    private Rigidbody2D _rb;
    private bool _canMove = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        MovePlayer();

    }

    // Move player in the horizontal axis
    private void MovePlayer()
    {
        if( _canMove)
        {
            _rb.velocity = new Vector2(_playerMoveInput.x * _playerSpeed, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        }
    }

    // Read the player horizontal and vertical Input
    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        _playerMoveInput = context.ReadValue<Vector2>();
        
    }

    // Move the player vertically, with the height depending on the duration the jump button is held
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            if (context.performed)
            {
                _clickTime = Time.time;
                _canMove = false;
            }
            if (context.canceled)
            {
                Debug.Log("Duration = " + (Time.time - _clickTime));
                _duration = Time.time - _clickTime;
                if(_duration < .5)
                {
                    _rb.velocity = new Vector2(0f, _jumpForce * .5f);
                }
                else if (_duration < 2 && _duration > .5)
                {
                    _rb.velocity = new Vector2(0f, _jumpForce * _duration);
                }
                else if(_duration > 2)
                {
                    _rb.velocity = new Vector2(0f, _jumpForce * 2f);
                }
                DecreasePlayerHP(_duration);
                _canMove = true;
            }
        }

    }

    // Decrease player HP based on the jump button hold duration
    private void DecreasePlayerHP(float holdDuration)
    {

        if ( holdDuration < .5)
        {
            _playerHP -= 5;

        }
        else if (holdDuration < 1f && holdDuration > .5)
        {
            _playerHP -= 10;

        }
        else if(holdDuration < 1.5f)
        {
            _playerHP -= 20;

        }
        else
        {
            _playerHP -= 30;

        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, .2f, GroundLayerMask);
    }
}
