using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Slider SoundSlider;
    public Slider MusicSlider;
    public GameObject TimerText;
    public GameObject HPUI;

    internal Vector2 PlayerPosition;

    [SerializeField] private TMP_Text hpValue;
    [SerializeField] private Slider hpSlider;
    public GameObject blackout;
    public bool inUI, deathscreenOpen;

    [SerializeField]
    private RectTransform _pauseMenu, _settingMenu, _uiButtons;

    [SerializeField]
    private Door _door;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        StopGame();
    }


    public void ChangeHpValue(int value) { 
        hpValue.text = value.ToString();
        hpSlider.value = value;
    }

    public void LowerCurtains() {
        deathscreenOpen = true;
        blackout.GetComponent<BlackoutScreen>().FadeInBlackout();
    }

    public void RaiseCurtains() {
        deathscreenOpen = false;
        blackout.GetComponent<BlackoutScreen>().FadeOutBlackout();
    }

    #region Button Behaviour

    private void StopGame()
    {
        if (GameManager.Instance.GameEnded) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !inUI)
        {
            if (!_door.InMenu)
            {
                OpenPauseMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inUI)
        { 
            ResumePlay();
        }

    }

    public void ResumePlay()
    {
        inUI = false;
        Time.timeScale = 1.0f;
        _pauseMenu.gameObject.SetActive(false);
        _settingMenu.gameObject.SetActive(false);
        if (deathscreenOpen)
        {
            blackout.SetActive(true);
        }
    }

    public void OpenPauseMenu() {
        inUI = true;
        Time.timeScale = 0f;
        _pauseMenu.gameObject.SetActive(true);
        _uiButtons.gameObject.SetActive(true);
        if (deathscreenOpen)
        {
            blackout.SetActive(false);
        }
    }

    //Opens settings from door and from pausemenu, settings is just volume
    public void OpenSettings()
    {
        inUI = true;
        Time.timeScale = 0f;
        _settingMenu.gameObject.SetActive(true);
        _uiButtons.gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }


    public void GoToMainMenu()
    {
        //SceneManager.LoadScene(0);
        bool deadOnArrival = deathscreenOpen;
        inUI = false;
        if (deathscreenOpen) {
            deathscreenOpen = false;
            blackout.transform.GetChild(0).gameObject.SetActive(false);
            blackout.transform.GetChild(1).gameObject.SetActive(false);
            blackout.SetActive(false);
        }
        PlayerManager.Instance.PlayerPosOnStop = PlayerManager.Instance.player.transform.position;
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _door.InitializeObjects(PlayerManager.Instance.CurrentPlayerHp, deadOnArrival);
        GetComponent<Timer>().PauseTimer();
    }

    public void CloseSettingMenu()
    {
        if (_door.InMenu)
        {
            inUI = false;
            Time.timeScale = 1f;
            _settingMenu.gameObject.SetActive(false);
        }
        else {
            _uiButtons.gameObject.SetActive(true);
            _settingMenu.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
