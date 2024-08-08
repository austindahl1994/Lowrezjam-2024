using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpForce = 10;
    private PlayerHP _playerHp;
    private PlayerParticles _particles;
    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;

    private bool facingRight = true;
    internal bool IsGrounded;
    private bool canDoubleJump = true;
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
    private bool isBouncing = false;
    private Vector2 rayOrigin;
    internal bool StoredValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerHp = GetComponent<PlayerHP>();
        _particles = GetComponent<PlayerParticles>();
    }

    private void Update()
    {
        if(!_playerHp.PlayerIsDead)
        {
            Move();
            Jump();
        }
        Bounce();
        if (IsGrounded)
            canDoubleJump = true;
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.2f), 0f, groundLayer);
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (!isBouncing)
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

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

    private void Jump()
    {
        float direction = rb.velocity.x > 0.1f ? 1f : (rb.velocity.x < -0.1f ? -1f : 0f);

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                SfxManager.Instance.PlayPlayerSfx("JumpSFX");
            }
            else
            {
                if (canDoubleJump)
                {
                    isBouncing = false;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    _playerHp.DecreasePlayerHP(5);
                    _particles.Spurt(direction);
                    canDoubleJump = false;
                }
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

        if (!IsGrounded)
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
