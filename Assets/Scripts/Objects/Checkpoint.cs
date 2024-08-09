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
        GameManager.Instance.checkpoints.Add(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lit || PlayerManager.Instance.PlayerDead) { 
            return;
        }
        if (collision.CompareTag("Player")) {
            GameManager.Instance.UpdateCheckpoint(transform.position, PlayerManager.Instance.CurrentPlayerHp);
            lit = true;
            animator.SetBool("Lit", true);
        }
    }
}
