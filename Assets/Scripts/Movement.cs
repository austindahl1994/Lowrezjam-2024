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
    private bool isGrounded;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerHp = GetComponent<PlayerHP>();
        _particles = GetComponent<PlayerParticles>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.5f, 0.2f), 0f, groundLayer);
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _playerHp.DecreasePlayerHP(5);
            _particles.Spurt(direction);
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
