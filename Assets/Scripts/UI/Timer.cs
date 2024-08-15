using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    internal string TimeText;

    [SerializeField] private TMP_Text timer;
    public float TimerValue { get; private set; }
    public bool timerRunning = false;
    public void StartTimer() { 
        timerRunning = true;
    }

    public void PauseTimer() { 
        timerRunning=false;
    }
    public void RestartTimer() { 
        TimerValue = 0;
    }

    public void AddTime() {
        TimerValue += 5;
    }

    private void Update()
    {
        if (timerRunning) { 
            TimerValue += Time.deltaTime;
        }
        int minutes = Mathf.FloorToInt(TimerValue / 60f);
        int seconds = Mathf.FloorToInt(TimerValue % 60f);
        int milliseconds = Mathf.FloorToInt((TimerValue * 10f) % 10f);

        timer.text = string.Format("{0:00}:{1:00}:{2:0}", minutes, seconds, milliseconds);
        TimeText = string.Format("{0:00}:{1:00}:{2:0}", minutes, seconds, milliseconds); 
    }
}
