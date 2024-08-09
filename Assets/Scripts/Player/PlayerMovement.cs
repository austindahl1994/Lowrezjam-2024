using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Player Movement")]

    [SerializeField]
    private float _playerSpeed;
    [SerializeField]
    private float _jumpForce;
    private float _clickTime, _duration;
    private Vector2 _playerMoveInput;
    private Rigidbody2D _rb;
    private bool _canMove = true;


    [Header("Ground Check")]
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundLayerMask;
    [SerializeField]
    private float _groundCheckRay = .01f;

    [Header("Wall Bounce")]
    [SerializeField]
    private float _forceAmount = 1f;
    [SerializeField]
    private float _rayLength = .1f;
    [SerializeField]
    private LayerMask _wallMask;

    private RaycastHit2D _raycastHitRight;
    private RaycastHit2D _raycastHitLeft;
    private bool _isBouncing = false;

    #endregion

    #region Unity Func
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_playerHp = GetComponent<PlayerHP>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
        Bounce();
        FlipPlayer();
    }
    #endregion

    # region Built up Methods
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
                PlayerManager.Instance.ChangeHp(1);
                _canMove = true;
            }
        }

    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRay, _groundLayerMask);
    }

    // Bounce off walls
    private void Bounce()
    {
        _raycastHitRight = Physics2D.Raycast(transform.position, transform.right, _rayLength, _wallMask);
        _raycastHitLeft = Physics2D.Raycast(transform.position, -transform.right, _rayLength, _wallMask);

        if(!IsGrounded())
        {
            if(_rb.velocity.y > 0)
            {
                if (_raycastHitRight)
                {
                   //Debug.Log("Right Wall Bounce");

                    _rb.AddForce((new Vector2(-1, 0) + new Vector2(0, 1)) * _forceAmount);
                    _isBouncing = true;
                }

                if (_raycastHitLeft)
                {
                    //Debug.Log("Left Wall Bounce");
                    _rb.AddForce((new Vector2(1, 0) + new Vector2(0, 1)) * _forceAmount);
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
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + _rayLength, transform.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - _rayLength, transform.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_groundCheck.position, new Vector2(_groundCheck.position.x , _groundCheck.position.y - _groundCheckRay));

    }
    #endregion
}
