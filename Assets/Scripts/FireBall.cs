using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private float _lifeDuration;
    [SerializeField]
    private float _projectilSpeed;

    private void Start()
    {
        // destroy the projectil after it's life duration expaired
        Invoke("DestroyBullet", _lifeDuration);
    }
    private void Update()
    {
        transform.Translate(Vector2.down * _projectilSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float angle = Vector2.Angle(-transform.up, PlayerManager.Instance.player.transform.right);
        //Debug.Log(angle);
        if(collision.CompareTag("Player"))
            PlayerManager.Instance.ChangeHp(1, false, false, 0, angle);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
