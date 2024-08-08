using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private Movement _movement;
    private PlayerHP _playerHP;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<Movement>();
        _playerHP = GetComponent<PlayerHP>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (_playerHP.PlayerIsDead)
        {
            _anim.Play("player_dead");

        }
        else
        {
            if (_movement.IsGrounded)
            {

                if (moveInput == 0)
                {
                    _anim.Play("player_idle");
                }
                else
                {
                    _anim.Play("player_walk");
                    SfxManager.Instance.PlayPlayerSfx("WalkSFX");

                }

            }
            else
            {
                if (_rb.velocity.y >= 0)
                {
                    _anim.Play("player_jump");
                }
                else
                {
                    _anim.Play("player_fall");
                }
            }
        }
       

    }
}
