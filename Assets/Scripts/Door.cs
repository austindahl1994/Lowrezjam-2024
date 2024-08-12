using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuTiles, _collisionTiles, _objectGrid, _menuCollTiles, _settingStatue;

    bool _firstTimeOPen = true;
    internal bool InMenu = true;
    Vector2 pos;
    private void Start()
    {
        pos = new(0.5f, -1.5f);
        InitializeObjects();
    }


    private void OnEnable()
    {
        InMenu = true;
    }

    private void OnDisable()
    {
        InMenu = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _menuTiles.SetActive(false);
            _menuCollTiles.SetActive(false);
            _collisionTiles.SetActive(true);
            _objectGrid.SetActive(true);
            if(_firstTimeOPen )
            {
                collision.gameObject.transform.position = new Vector2(pos.x, pos.y + 0.1f);
                _firstTimeOPen = false;
            }
            else
            {
                PlayerManager.Instance.player.transform.position= PlayerManager.Instance.PlayerPosOnStop;
                UIManager.Instance.GetComponent<Timer>().StartTimer();
            }

            PlayerManager.Instance.CanLowerHP = true;
            UIManager.Instance.TimerText.SetActive(true);
            UIManager.Instance.HPUI.SetActive(true);
            _settingStatue.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void InitializeObjects()
    {
        _collisionTiles.SetActive(false);
        _objectGrid.SetActive(false);
        _menuTiles.SetActive(true);
        _menuCollTiles.SetActive(true);
        _settingStatue.SetActive(true);
        gameObject.SetActive(true);
        UIManager.Instance.TimerText.SetActive(false);
        UIManager.Instance.HPUI.SetActive(false);
        PlayerManager.Instance.player.transform.position = new Vector2(pos.x, pos.y + 0.1f);
        PlayerManager.Instance.CanLowerHP = false;

    }
}
