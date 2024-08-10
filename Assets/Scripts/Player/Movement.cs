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
    public bool gettingHit;
    public bool isBouncing;

    [Header("Wall Bounce")]
    [SerializeField]
    private float forceAmount = 1f;
    [SerializeField]
    private float rayLength = .1f;
    [SerializeField]
    private LayerMask wallMask;
    [SerializeField]
    private float rayOffSet;
    private RaycastHit2D raycastHitRight;
    private RaycastHit2D raycastHitLeft;
    private Vector2 rayOrigin;
    internal bool StoredValue;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        canMove = true;
        isBouncing = false;
        gettingHit = false;
    }

    private void Update()
    {
        if (!canMove || PlayerManager.Instance.PlayerDead) {
            return;
        }
        Move();
        Jump();
        Bounce();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.2f), 0f, groundLayer);
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isBouncing && !gettingHit) {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        if (rb.velocity.x == 0 && isGrounded)
        {
            //playerState.SetState(0);
        }
        else if (Mathf.Abs(rb.velocity.x) >= 0 && isGrounded)
        {
            //playerState.SetState(1);
        }
        else
        {
            //playerState.SetState(2);
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
                //jumpSound.PlayAudio();
                SoundManager.Instance.PlayPlayerSfx("JumpSFX");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount = 1;
                canDoubleJump = true;
                //playerState.SetState(2);
            }
            else if (canDoubleJump && jumpCount < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;
                canDoubleJump = false;
                isBouncing = false;
                PlayerManager.Instance.ChangeHp(1, false, false, 0, direction == 0 ? 90 : (direction == 1 ? -135f : 45f));
                //playerState.SetState(2);
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

    // Bounce off walls
    private void Bounce()
    {
        rayOrigin = new Vector2(transform.position.x, transform.position.y + rayOffSet);
        raycastHitRight = Physics2D.Raycast(transform.position, transform.right, rayLength, wallMask);
        raycastHitLeft = Physics2D.Raycast(transform.position, -transform.right, rayLength, wallMask);

        if (!isGrounded)
        {
            if (rb.velocity.y > 0)
            {
                if (raycastHitRight)
                {
                    //Debug.Log("Right Wall Bounce");

                    rb.AddForce((new Vector2(-1, 0) + new Vector2(0, 1)) * forceAmount);
                    isBouncing = true;
                }

                if (raycastHitLeft)
                {
                    // Debug.Log("Left Wall Bounce");
                    rb.AddForce((new Vector2(1, 0) + new Vector2(0, 1)) * forceAmount);
                    isBouncing = true;
                }
            }
        }
        else
        {
            isBouncing = false;
        }
    }


    private void OnDrawGizmos()
    {
        rayOrigin = new Vector2(transform.position.x, transform.position.y + rayOffSet);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin, new Vector2(rayOrigin.x + rayLength, rayOrigin.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayOrigin, new Vector2(rayOrigin.x - rayLength, rayOrigin.y));

    }
}
