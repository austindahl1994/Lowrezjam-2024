using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpForce = 10;
    private PlayerState playerState;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public SoundSO jumpSound;
    private Rigidbody2D rb;

    private bool facingRight = true;
    public bool isGrounded;
    public bool canMove;
    public bool canDoubleJump;
    public int jumpCount;
    public bool isBouncing;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        canMove = true;
        isBouncing = false;
    }

    private void Update()
    {
        if (!canMove) {
            return;
        }
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.2f), 0f, groundLayer);
    }

    private void Move()
    {
        if (!isBouncing) {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (rb.velocity.x == 0 && isGrounded)
            {
                playerState.SetState(0);
            }
            else if (Mathf.Abs(rb.velocity.x) >= 0 && isGrounded)
            {
                playerState.SetState(1);
            }
            else
            {
                playerState.SetState(2);
            }
            //Debug.Log(rb.velocity.x);
            float clampedX = Mathf.Clamp(rb.position.x, -3.64f, 3.64f);

            rb.position = new Vector2(clampedX, transform.position.y);
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    public void ResetDoubleJump() { 
        jumpCount = 0;
        canDoubleJump = true;
    }

    private void Jump()
    {
        float direction = rb.velocity.x > 0.1f ? 1f : (rb.velocity.x < -0.1f ? -1f : 0f);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpSound.PlayAudio();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount = 1;
                canDoubleJump = true;
                playerState.SetState(2);
            }
            else if (canDoubleJump && jumpCount < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;
                canDoubleJump = false;
                PlayerManager.Instance.ChangeHp(1, false, false, 0, direction == 0 ? 90 : (direction == 1 ? -135f : 45f));
                playerState.SetState(2);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
