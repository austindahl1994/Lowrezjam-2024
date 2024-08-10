using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Mimic : Platform
{
    [SerializeField]
    private GameObject player;

    private Transform fake;
    private Transform real;
    private Transform eye;
    private float targetAngle;
    private bool isPlayerInside = false;

    protected override void Start()
    {
        base.Start();
        if (player == null)
        {
            player = PlayerManager.Instance.player;
        }
        fake = gameObject.transform.GetChild(0);
        real = gameObject.transform.GetChild(1);
        eye = real.gameObject.transform.GetChild(0);
    }

    private void Update()
    {
        WatchPlayer();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyManager.Instance.mimics.Add(transform);
            fake.gameObject.SetActive(false);
            real.gameObject.SetActive(true);
            isPlayerInside = true;
            StartCoroutine(CheckPlayerStatusAfterDelay(1f));
        }
    }

    private IEnumerator CheckPlayerStatusAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isPlayerInside)
        {
            real.GetComponent<Animator>().SetTrigger("Bite");
            PlayerManager.Instance.ChangeHp(0, true, false, 1);
        }
        else
        {
            fake.gameObject.SetActive(false);
            real.gameObject.SetActive(true);
        }
    }

    private void WatchPlayer()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 objectPos = transform.position;

        if (playerPos.y > objectPos.y)
        {
            if (playerPos.x <= objectPos.x - 1)
            {
                // NW
                targetAngle = 90f;
            }
            else if (playerPos.x >= objectPos.x + 1)
            {
                // NE
                targetAngle = 0f;
            }
        }
        else
        {
            if (playerPos.x <= objectPos.x - 1)
            {
                // SW
                targetAngle = 180;
            }
            else if (playerPos.x >= objectPos.x + 1)
            {
                // SE
                targetAngle = 270;
            }
        }

        eye.eulerAngles = new Vector3(0, 0, targetAngle);
    }
}
