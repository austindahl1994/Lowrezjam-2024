using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance;
    [SerializeField]
    private Animator _endScreenAnim;
    [SerializeField]
    private Charon _charon;
    [SerializeField]
    private GameObject _mainCam, _endCam, _menuButton;
    [SerializeField]
    private TextMeshProUGUI _deathCounter, _timerText;
    public bool canStartText;
    public bool startFadeOut;

    [SerializeField]
    private float _textAppearanceDuration = 2;

    private bool _doItOnce = true, _charonCanMove= false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        ManageGameEnding();
    }
    private void ManageGameEnding()
    {
        if (GameManager.Instance.GameEnded)
        {
            if (_doItOnce)
            {
                UIManager.Instance.HPUI.SetActive(false);
                UIManager.Instance.TimerText.SetActive(false);
                _endScreenAnim.gameObject.SetActive(true);
                _endScreenAnim.Play("endScreen_blackIn");
                if (_endScreenAnim.GetCurrentAnimatorStateInfo(0).IsName("endScreen_blackIn") &&
                    _endScreenAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
                {
                    _mainCam.SetActive(false);
                    _endCam.SetActive(true);
                    _endScreenAnim.Play("endScreen_blackOut");
                    _deathCounter.text += PlayerManager.Instance.PlayerDeathCount.ToString();
                    _timerText.text += UIManager.Instance.GetComponent<Timer>().TimeText;
                    SoundManager.Instance.StopSFX();
                    _charonCanMove = true;
                    _doItOnce = false;
                }
            }

            if(_charonCanMove)
            {
                _charon.transform.GetChild(0).gameObject.SetActive(false);
                _charon.GetComponent<SpriteRenderer>().enabled = true;
                _charon.MoveCharon();
            }
            CharonPosition();
            ShowText();
            StartFade();
        }
        else
        {
            _endScreenAnim.gameObject.SetActive(false);
            _mainCam.SetActive(true);
            _endCam.SetActive(false);
            _menuButton.SetActive(false);
            _timerText.gameObject.SetActive(false);
            _deathCounter.gameObject.SetActive(false);
            canStartText = false;
            startFadeOut = false;
        }
    }

    private void CharonPosition() {
        if (_charon.transform.position.x > 10) { 
            canStartText = true;
        }
        if (_charon.transform.position.x > 15)
        {
            if (!startFadeOut) {
                _textAppearanceDuration = 3.0f;
                startFadeOut = true;
            }
        }
    }

    private void ShowText() {
        if (canStartText) {
            _textAppearanceDuration -= Time.deltaTime;

            if (_textAppearanceDuration <= 5.5)
            {
                _deathCounter.gameObject.SetActive(true);
            }
            if (_textAppearanceDuration <= 4)
            {
                _timerText.gameObject.SetActive(true);
            }
        }
    }

    private void StartFade() {
        if (startFadeOut) {
            _endScreenAnim.Play("endScreen_blackIn");
            if (_textAppearanceDuration <= 0)
            {
                _menuButton.SetActive(true);
            }
        }
    }
}
