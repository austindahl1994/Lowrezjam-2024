using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : MonoBehaviour
{
    public GameObject FireBall, Center;
    [SerializeField]
    private float _timeBetweenFireBall;

    private float _initiationTime;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();    
    }

    private void Update()
    {
        InitializeFireBall();
    }

    private void InitializeFireBall()
    {
        if (_timeBetweenFireBall + _initiationTime < Time.time)
        {
            _anim.Play("imp_attack");
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("imp_attack") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9)
            {
                GameObject.Instantiate(FireBall, Center.transform.position, Center.transform.rotation);
                _initiationTime = Time.time;
                _anim.Play("imp_idle");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerManager.Instance.ChangeHp(1, false, false, 0, 90);
    }
}
