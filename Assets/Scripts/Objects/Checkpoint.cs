using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    public bool lit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lit || PlayerManager.Instance.PlayerDead) { 
            return;
        }
        if (collision.CompareTag("Player")) {
            GameManager.Instance.checkpoints.Add(transform);
            GameManager.Instance.UpdateCheckpoint(transform.position, PlayerManager.Instance.CurrentPlayerHp);
            lit = true;
            animator.SetBool("Lit", true);
        }
    }

    public void ResetCheckpoint() {
        lit = false;
        animator.SetBool("Lit", false);
    }
}
