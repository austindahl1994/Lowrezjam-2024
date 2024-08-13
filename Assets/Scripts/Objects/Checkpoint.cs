using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    public bool lit = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lit || PlayerManager.Instance.PlayerDead) { 
            return;
        }
        if (collision.CompareTag("Player")) {
            GameManager.Instance.checkpoints.Add(transform);
            GameManager.Instance.UpdateCheckpoint(transform.position);
            lit = true;
            animator.SetBool("Lit", true);
        }
    }

    public void ResetCheckpoint() {
        lit = false;
        animator.SetBool("Lit", false);
    }
}
