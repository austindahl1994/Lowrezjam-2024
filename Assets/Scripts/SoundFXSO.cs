using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SoundSO/Sfx")]
public class SoundSO : ScriptableObject
{
    public string AudioName;
    public List<AudioClip> Clips;
    [Range(0f, 10f)]
    public float Volume;

    public void PlayAudio()
    {
        if (Clips != null && Clips.Count > 0)
        {
            AudioClip selectedClip = Clips.Count > 1 ? Clips[Random.Range(0, Clips.Count)] : Clips[0];

            if (selectedClip != null)
            {
                AudioSource tempAudioSource = new GameObject("TempAudio").AddComponent<AudioSource>();
                tempAudioSource.clip = selectedClip;
                tempAudioSource.volume = Volume;
                tempAudioSource.spatialBlend = 0f;
                tempAudioSource.Play();
                Destroy(tempAudioSource.gameObject, selectedClip.length);
            }
        }
    }
}
