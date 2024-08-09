using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text hpValue;
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeHpValue(int value) { 
        hpValue.text = value.ToString();
        hpSlider.value = value;
    }
}
