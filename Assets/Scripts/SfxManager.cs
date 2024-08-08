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
    public float PlayerSfxClipLength(string SfxName)
    {
        return FindSfx(_playerSfx, SfxName).Clip.length;
    }

    // Play a specific Sfx by giving it's name and the list it belong to
    private void PlaySfx(string audioName, SoundSO[] sfxList)
    {
        // Find the sfx clip in the givin list with the provided name
        SoundSO audioClip = FindSfx(sfxList, audioName);
        // Create an audio source in the scene with the clip that we found
        AudioSource audioSource = Instantiate(_audioSource, transform.position, Quaternion.identity);
        // We assign the clip and it's volume to the audio source
        audioSource.clip = audioClip.Clip;
        audioSource.volume = audioClip.Volume;
        // we play the clip
        audioSource.Play();
        // we destroy the audio source when the clip end playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
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
