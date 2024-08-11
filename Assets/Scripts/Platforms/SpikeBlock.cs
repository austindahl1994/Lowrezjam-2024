using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    private int rotateValue;
    private readonly float bounceForce = 5f;
    private void Start()
    {
        rotateValue = 3;
    }
    public void Rotate() {
        rotateValue = (rotateValue + 1) % 4;
        //Debug.Log($"Rotate called with value {rotateValue}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.ChangeHp(1, false, false, 0, (rotateValue * 90) % 360);
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            StartCoroutine(HandleBounce(rb, collision));
        }
    }

    private IEnumerator HandleBounce(Rigidbody2D rb, Collider2D collision)
    {
        collision.gameObject.GetComponent<Movement>().gettingHit = true;
        if (rotateValue == 3 || rotateValue == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, rotateValue == 3 ? bounceForce : -bounceForce);
        }
        else
        {
            rb.velocity = new Vector2(rotateValue == 2 ? -bounceForce : bounceForce, rb.velocity.y);
        }

        yield return new WaitForSeconds(0.3f);

        collision.gameObject.GetComponent<Movement>().gettingHit = false;
    }

}
