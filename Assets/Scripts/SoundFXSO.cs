using UnityEngine;

[CreateAssetMenu(menuName = "SoundSO/ Sfx")]
public class SoundSO : ScriptableObject
{
    public string AudioName;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume;
}
