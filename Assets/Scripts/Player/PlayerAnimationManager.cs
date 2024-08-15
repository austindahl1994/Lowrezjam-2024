using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private Movement _movement;
    //private float time;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (PlayerManager.Instance.PlayerDead)
        {
            _anim.Play("player_dead");

        }
        else
        {
            if (_movement.isGrounded)
            {

                if (moveInput == 0 || GameManager.Instance.GameEnded)
                {
                    _anim.Play("player_idle");
                }
                else
                {
                    _anim.Play("player_walk");
                    //PlaySFX("WalkSFX");
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

    /*
    private void PlaySFX(string sfxName)
    {
        if ((Time.time - SoundManager.Instance.PlayerSfxClipLength(sfxName) >= time))
        {
            SoundManager.Instance.PlayPlayerSfx(sfxName);
            time = Time.time;
        }
    }*/
}
