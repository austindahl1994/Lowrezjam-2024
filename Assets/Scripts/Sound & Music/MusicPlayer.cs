using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private SoundSO _music;
    [SerializeField]
    private Door _door;

    private AudioSource _audioSource;
    private bool _playOnce;

    private void Start()
    {
        UIManager.Instance.MusicSlider.value = 5;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _music.Clips[0];
        _audioSource.volume = _music.Volume;
        _audioSource.Play();
    }
    private void Update()
    {
        _audioSource.volume = _music.Volume;
        ManageMusic();
    }

    public void ChangeMusic(int index)
    {
        _audioSource.clip = _music.Clips[index];

        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        

    }

    public void AdjustVolume()
    {
        //Debug.Log("volume = " + UIManager.Instance.SoundSlider.value);
        _music.Volume = UIManager.Instance.MusicSlider.value / 10;
        
    }

    private void ManageMusic()
    {
        if (_door.InMenu)
        {
            ChangeMusic(0);
            _audioSource.loop = true;
        }
        else
        {
            if (PlayerManager.Instance.PlayerDead)
            {
                if(_playOnce)
                {
                    ChangeMusic(2);
                    _playOnce = false;
                }
                _audioSource.loop = false;
                if(!_audioSource.isPlaying)
                {
                    ChangeMusic(3);
                }

            }
            else
            {
                _playOnce = true;
                _audioSource.loop = true;
                if (GameManager.Instance.GameEnded)
                {
                    ChangeMusic(4);
                }
                else
                {
                    ChangeMusic(1);
                }
            }
        }
    }

}
