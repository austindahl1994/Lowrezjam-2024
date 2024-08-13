using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public SoundSO _hoverUISound;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponentInParent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    private void Hover()
    {
        _audioSource.clip = _hoverUISound.Clips[0];
        _audioSource.volume = _hoverUISound.Volume;
        _audioSource.Play();
    }

    private void Click()
    {
        GetComponentInParent<CanvasSounds>().Click();
    }
}
