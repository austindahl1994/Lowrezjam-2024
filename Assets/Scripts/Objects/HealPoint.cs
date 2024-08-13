using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerManager.Instance.ChangeHp(0, false, true);
            SoundManager.Instance.PlayPlayerSfx("Holy Heal");
        }
    }
}
