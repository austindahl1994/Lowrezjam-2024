using System;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    #region Variables

    public static SfxManager Instance;

    [Header("Player Sfx List")]
    [SerializeField]
    private SoundSO[] _playerSfx;

    [Header("Audio Source")]
    [SerializeField]
    private AudioSource _audioSource;
    private float time;


    #endregion

    #region Unity Func

    private void Awake()
    {
        Instance = this;
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
            audioSource.volume = sound.Volume;
            audioSource.spatialBlend = 0f;

            audioSource.Play();

            Destroy(audioSource.gameObject, selectedClip.length);
        }
    }


    #endregion

    #region Player Sfx Func

    public void PlayPlayerSfx(string sfxName)
    {
        if ((Time.time - PlayerSfxClipLength(sfxName) >= time))
        {
            PlaySfx(sfxName, _playerSfx);
            time = Time.time;
        }
    }
    #endregion

    #endregion
}
