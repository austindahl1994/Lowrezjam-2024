using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{

    [SerializeField]
    private float _healthPoints = 100;
    public TextMeshProUGUI HpText;
    internal bool PlayerIsDead = false;

    private void Update()
    {
        HpText.text = "HP: " + _healthPoints;
    }

    // Decrease player HP based on the jump button hold duration
    public void DecreasePlayerHP(float holdDuration)
    {
        /*
        if (holdDuration < .5)
        {
            _healthPoints -= 5;

        }
        else if (holdDuration < 1f && holdDuration > .5)
        {
            _healthPoints -= 10;

        }
        else if (holdDuration < 1.5f)
        {
            _healthPoints -= 20;

        }
        else
        {
            _healthPoints -= 30;

        }*/
        _healthPoints -= holdDuration;
        CheckHealth();
    }

    public void IncreasePlayerHP(int amount)
    {
        if(_healthPoints < 100)
        {
            _healthPoints += amount;
        }
        
    }

    private void CheckHealth()
    {
        if(_healthPoints <= 0)
        {
            PlayerIsDead = true;
        }
    }

}
