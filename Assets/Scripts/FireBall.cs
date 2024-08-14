using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float _lifeDuration;
    [SerializeField]
    private float _projectilSpeed;
    [SerializeField]
    private SoundSO _fireballSFX;
    private bool hasDamagedPlayer = false;

    private void Start()
    {
        // destroy the projectil after it's life duration expaired
        Invoke("DestroyBullet", _lifeDuration);
        GetComponent<AudioSource>().clip = _fireballSFX.Clips[0];
        GetComponent<AudioSource>().volume = _fireballSFX.Volume;
        GetComponent<AudioSource>().Play();

    }
    private void Update()
    {
        transform.Translate(Vector2.left * _projectilSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasDamagedPlayer) {
            return;
        }
        float angle = Vector2.Angle(-transform.up, PlayerManager.Instance.player.transform.right);
        //Debug.Log(angle);
        if (collision.CompareTag("Player")) {
            hasDamagedPlayer = true;
            PlayerManager.Instance.ChangeHp(1, false, false, 0, angle);
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            StartCoroutine(HandleBounce(rb, collision));
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private IEnumerator HandleBounce(Rigidbody2D rb, Collider2D collision)
    {
        int force = GetComponentInParent<Imp>().facingRight ? 7 : -7;
        collision.gameObject.GetComponent<Movement>().gettingHit = true;
        rb.velocity = new Vector2(force, rb.velocity.y);
        yield return new WaitForSeconds(0.3f);

        collision.gameObject.GetComponent<Movement>().gettingHit = false;
    }
}
