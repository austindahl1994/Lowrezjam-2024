using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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


    public float ForceAmount = 1f;
    public float RayLength = .1f;
    private RaycastHit2D _raycastHitRight;
    private RaycastHit2D _raycastHitLeft;

    public LayerMask WallMask;
    private bool _isBouncing = false;

    public TextMeshProUGUI HpText;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HpText.text = "HP: " + _playerHP;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        Bounce();
        FlipPlayer();
    }

    // Move player in the horizontal axis
    private void MovePlayer()
    {
        if( _canMove )
        {
            if(!_isBouncing)
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
                //Debug.Log("Duration = " + (Time.time - _clickTime));
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
        return Physics2D.OverlapCircle(GroundCheck.position, 0.05f, GroundLayerMask);
    }

    // Bounce off walls
    private void Bounce()
    {
        _raycastHitRight = Physics2D.Raycast(transform.position, transform.right, RayLength, WallMask);
        _raycastHitLeft = Physics2D.Raycast(transform.position, -transform.right, RayLength, WallMask);

        if(!IsGrounded())
        {
            if(_rb.velocity.y > 0)
            {
                if (_raycastHitRight)
                {
                    Debug.Log("Right Wall Bounce");

                    _rb.AddForce((new Vector2(-1, 0) + new Vector2(0, 1)) * ForceAmount);
                    _isBouncing = true;
                }

                if (_raycastHitLeft)
                {
                    Debug.Log("Left Wall Bounce");
                    _rb.AddForce((new Vector2(1, 0) + new Vector2(0, 1)) * ForceAmount);
                    _isBouncing = true;
                }
            }
        }
        else
        {
            _isBouncing = false;
        }
    }

    // flip the player based on the direction he want to move in
    private void FlipPlayer()
    {
        Vector2 localScale = transform.localScale;

        if (_playerMoveInput.x > 0)
        {
            localScale = new Vector2(Mathf.Abs(localScale.x), localScale.y);
        }
        else if (_playerMoveInput.x < 0)
        {
            localScale = new Vector2(-Mathf.Abs(localScale.x), localScale.y);
        }

        transform.localScale = localScale;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + RayLength, transform.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - RayLength, transform.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x , transform.position.y - 0.05f));

    }
}
