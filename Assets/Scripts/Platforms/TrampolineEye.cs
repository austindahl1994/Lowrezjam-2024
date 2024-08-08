using UnityEngine;
using System.Collections;

public class TrampolineEye : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 10f;

    [SerializeField]
    private Player player;

    private Vector2 playerPos;
    private Vector2 objectPos;

    [SerializeField]
    private Sprite[] eyes;

    public AudioClip bounceSound;
    public Animator trampolineAnimator;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
    private void Start()
    {
        sr = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
        StartCoroutine(Blink());
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer() { 
        playerPos = player.transform.position;
        objectPos = transform.position;

        if (playerPos.y > objectPos.y)
        {
            if (playerPos.x <= objectPos.x - 1)
            {
                // NW
                sr.sprite = eyes[0];
            }
            else if (playerPos.x >= objectPos.x + 1)
            {
                // NE
                sr.sprite = eyes[2];
            }
            else
            {
                sr.sprite = eyes[1];
            }
        }
        else
        {
            if (playerPos.x <= objectPos.x - 1)
            {
                // SW
                sr.sprite = eyes[5];
            }
            else if (playerPos.x >= objectPos.x + 1)
            {
                // SE
                sr.sprite = eyes[3];
            }
            else
            {
                sr.sprite = eyes[4];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            if (other.TryGetComponent<Animator>(out Animator animator))
            {
                animator.Play("Idle");
            }
            if (trampolineAnimator != null)
            {
                trampolineAnimator.SetTrigger("Blink");
            }
            /* Add when sound effect is available
             if (bounceSound != null)
            {
                AudioSource.PlayClipAtPoint(bounceSound, transform.position); 
            }
             */
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            float randomTime = Random.Range(7f, 10f);
            yield return new WaitForSeconds(randomTime);
            trampolineAnimator.SetTrigger("Blink");
        }
    }
}
