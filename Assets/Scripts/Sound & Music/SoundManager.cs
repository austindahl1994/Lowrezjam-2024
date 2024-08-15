using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Variables

    public static SoundManager Instance;

    [Header("Player Sfx List")]
    [SerializeField]
    private SoundSO[] _playerSfx;

    [Header("Audio Source")]
    [SerializeField]
    private AudioSource _audioSource;

    #endregion

    #region Unity Func

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.SoundSlider.value = 5;

        InitializeVolume();
    }

    private void Update()
    {
        if (PlayerManager.Instance.player.transform.position.y > 120 && !GameManager.Instance.GameEnded) {
            _playerSfx[8].Volume = Mathf.Clamp((UIManager.Instance.SoundSlider.value / 10) - 0.2f, 0, 1);
        }
    }

    #endregion

    #region Built Func

    #region General Funcs
    // Find a specific Sfx by giving it's name and the list it belong to
    private SoundSO FindSfx(SoundSO[] soundList, string clipName)
    {
        SoundSO result = Array.Find(soundList, x => x.AudioName == clipName);
        return result;
    }

    // return the length of a player sfx clip
    public float PlayerSfxClipLength(string sfxName)
    {
        SoundSO sound = FindSfx(_playerSfx, sfxName);

        if (sound != null && sound.Clips.Count > 0)
        {
            return sound.Clips[0].length;
        }

        Debug.LogWarning("No clips found for " + sfxName);
        return 0f;
    }


    // Play a specific Sfx by giving it's name and the list it belong to
    private void PlaySfx(string audioName, SoundSO[] sfxList)
    {
        SoundSO sound = FindSfx(sfxList, audioName);

        if (sound != null && sound.Clips.Count > 0)
        {

            AudioClip selectedClip = sound.Clips[UnityEngine.Random.Range(0, sound.Clips.Count)];
            AudioSource audioSource = Instantiate(_audioSource, transform.position, Quaternion.identity);
            audioSource.clip = selectedClip;
            audioSource.volume = sound.Volume ;
            //Debug.Log("volume = " + audioSource.volume);
            audioSource.spatialBlend = 0f;

            audioSource.Play();

            Destroy(audioSource.gameObject, selectedClip.length);


        }
    }


    #endregion

    #region Player Sfx Func

    public void PlayPlayerSfx(string sfxName)
    {
        //Unnecessary?
        //float clipLength = PlayerSfxClipLength(sfxName);
        PlaySfx(sfxName, _playerSfx);
        
    }

    public void ChangeSoundVolum()
    {
        //Debug.Log("volume = " + UIManager.Instance.SoundSlider.value);

        foreach (var sound in _playerSfx)
        {
            sound.Volume = UIManager.Instance.SoundSlider.value /10;
        }
    }

    private void InitializeVolume()
    {
        foreach (var sound in _playerSfx)
        {
            sound.Volume = UIManager.Instance.SoundSlider.value / 10 ;
        }
    }

    public void StopSFX() {
        foreach (var sound in _playerSfx)
        {
            sound.Volume = 0;
        }
    }

    #endregion

    #endregion
}
