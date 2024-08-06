using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerHP _playerHp;
    [SerializeField]
    private int _healingAmount = 1;
    [SerializeField]
    private float _timeBetweelHeal = 1f;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _checkDistance;


    private float _healMoment;
    private bool _canHeal = false;

    private void Update()
    {
        CheckPlayer();
        if (_canHeal)
        {
            HealPlayer();
        }
    }

    // Check if player is near enough to heal him
    private void CheckPlayer()
    {
        if(Vector2.Distance(transform.position, _player.position) <= _checkDistance)
        {
            _canHeal = true;
        }
        else
        {
            _canHeal = false;
        }
    }

    // Heal player every x period of time
    private void HealPlayer()
    {
        if(Time.time >  _timeBetweelHeal + _healMoment)
        {
            _playerHp.IncreasePlayerHP(_healingAmount);
            _healMoment = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _checkDistance);
    }
}
