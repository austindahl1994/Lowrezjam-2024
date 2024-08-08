using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    private float _healthPoints = 100;
    public TextMeshProUGUI HpText;
    private Player player;
    private Movement move;
    public bool PlayerIsDead = false;

    private void Start()
    {
        player = GetComponent<Player>();
        move = GetComponent<Movement>();
    }

    private void Update()
    {
        HpText.text = "HP: " + _healthPoints;
    }

    // Decrease player HP based on the jump button hold duration
    public void DecreasePlayerHP(float holdDuration)
    {
        if (_healthPoints - holdDuration <= 0)
        {
            _healthPoints = 0;
            player.SetState(3);
            move.canMove = false;
            PlayerIsDead = true;
        }
        else {
            _healthPoints -= holdDuration;
        }
    }

    public void FullHeal()
    {
        if (_healthPoints <= 0) {
            return;
        }
        _healthPoints = 100;
    }

    public void KillPlayer() {
        _healthPoints = 0;
    }
}
