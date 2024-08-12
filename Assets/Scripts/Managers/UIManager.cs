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
    public GameObject TimerText;
    public GameObject HPUI;

    internal Vector2 PlayerPosition;

    [SerializeField] private TMP_Text hpValue;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject blackout;

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
        //Debug.Log("Change HP UIManager called");
    }

    public void LowerCurtains() {
        blackout.GetComponent<BlackoutScreen>().FadeInBlackout();
    }

    public void RaiseCurtains() {
        blackout.GetComponent<BlackoutScreen>().FadeOutBlackout();
    }

    #region Button Behaviour

    private void StopGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            _pauseMenu.gameObject.SetActive(true);
        }

    }

    public void ResumePlay()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        _settingMenu.gameObject.SetActive(true);
        _uiButtons.gameObject.SetActive(false);

    }

    public void GoToMainMenu()
    {
        //SceneManager.LoadScene(0);
        PlayerManager.Instance.PlayerPosOnStop = PlayerManager.Instance.player.transform.position;
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        _door.InitializeObjects();
        GetComponent<Timer>().PauseTimer();
    }

    public void CloseSettingMenu()
    {
        _uiButtons.gameObject.SetActive(true);
        _settingMenu.gameObject.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
