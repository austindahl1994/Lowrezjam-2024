using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasSounds : MonoBehaviour
{
    private List<GameObject> btnList = new();
    private AudioSource _audioSource;
    [SerializeField] private SoundSO _hoverUISound, _clickUISound;
    private void Awake()
    {
        foreach (Transform t in transform) {
            FindButtonsInChildren(t);
        }
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (GameObject b in btnList)
        {
            b.AddComponent<ButtonSounds>();
            b.GetComponent<ButtonSounds>()._hoverUISound = _hoverUISound;
        }
    }

    void FindButtonsInChildren(Transform parent)
    {
        Button button = parent.GetComponent<Button>();

        if (button != null)
        {
            btnList.Add(parent.gameObject);
        }

        foreach (Transform child in parent)
        {
            FindButtonsInChildren(child);
        }
    }

    public void Click()
    {
        _audioSource.clip = _clickUISound.Clips[0];
        _audioSource.volume = _clickUISound.Volume;
        _audioSource.Play();
    }
}
