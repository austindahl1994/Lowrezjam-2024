using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private SoundSO _music;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _music.Clips[0];
        _audioSource.volume = _music.Volume;
        _audioSource.Play();
    }
    private void Update()
    {
        _audioSource.volume = _music.Volume;
    }

    public void ChangeMusic(int index)
    {
        _audioSource.clip = _music.Clips[index];
        _audioSource.Play();
    }

    public void AdjustVolume()
    {
        //Debug.Log("volume = " + UIManager.Instance.SoundSlider.value);

        _music.Volume = UIManager.Instance.MusicSlider.value / 10;
        
    }
}
