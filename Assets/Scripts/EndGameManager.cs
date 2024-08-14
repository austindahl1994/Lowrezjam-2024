using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private Animator _endScreenAnim;
    [SerializeField]
    private Charon _charon;
    [SerializeField]
    private GameObject _mainCam, _endCam, _menuButton;
    [SerializeField]
    private TextMeshProUGUI _deathCounter, _timerText;


    [SerializeField]
    private float _textAppearanceDuration = 2;

    private bool _doItOnce = true, _charonCanMove= false;
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

                    _charonCanMove = true;
                    _doItOnce = false;
                }
            }

            if(_charonCanMove)
            {
                _charon.MoveCharon();
            }

            _textAppearanceDuration -= Time.deltaTime;

            if (_textAppearanceDuration <= 5.5)
            {
                _deathCounter.gameObject.SetActive(true);
            }
            if (_textAppearanceDuration <= 4)
            {
                _timerText.gameObject.SetActive(true);
            }
            if (_textAppearanceDuration <= 3)
            {
                _menuButton.SetActive(true);
            }
        }
        else
        {
            _endScreenAnim.gameObject.SetActive(false);
            _mainCam.SetActive(true);
            _endCam.SetActive(false);
            _menuButton.SetActive(false);
            _timerText.gameObject.SetActive(false);
            _deathCounter.gameObject.SetActive(false);

        }

    }
}
