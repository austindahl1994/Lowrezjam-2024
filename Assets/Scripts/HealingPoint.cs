using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private PlayerHP hp;
    private void Start()
    {
        hp = player.GetComponent<PlayerHP>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            hp.FullHeal();
        }
    }
}
