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
    private bool byCollider, toggled = false;
    private bool deadOnArrival = false;
    Vector2 pos;
    private void Start()
    {
        pos = new(0.5f, -1.5f);
        InitializeObjects();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && byCollider)
        {
            StartPlaying();
        }
        if (InMenu) { 
            UIManager.Instance.GetComponent<Timer>().timerRunning = false;
        }
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
            byCollider = true;
            ToggleChildren();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            byCollider = false;
            ToggleChildren();
        }
    }

    public void InitializeObjects(int hp = 3, bool dead = false)
    {
        deadOnArrival = dead;
        _collisionTiles.SetActive(false);
        _objectGrid.SetActive(false);
        _menuTiles.SetActive(true);
        _menuCollTiles.SetActive(true);
        _settingStatue.SetActive(true);
        gameObject.SetActive(true);
        UIManager.Instance.TimerText.SetActive(false);
        UIManager.Instance.HPUI.SetActive(false);
        PlayerManager.Instance.InitializePlayer(new Vector2(pos.x, pos.y + 0.1f), hp);
    }

    public void StartPlaying() {
        _menuTiles.SetActive(false);
        _menuCollTiles.SetActive(false);
        _collisionTiles.SetActive(true);
        _objectGrid.SetActive(true);
        if (_firstTimeOPen)
        {
            _firstTimeOPen = false;
            PlayerManager.Instance.InitializePlayer(new Vector2(pos.x, pos.y + 0.1f));
        }
        else
        {
            if (deadOnArrival)
            {
                if (GameManager.Instance.checkpoints.Count == 0)
                {
                    PlayerManager.Instance.InitializePlayer(new Vector2(pos.x, pos.y + 0.1f), 3);
                }
                else
                {
                    PlayerManager.Instance.InitializePlayer(GameManager.Instance.latestCheckpointPosition, 3);
                }
            }
            else {
                PlayerManager.Instance.InitializePlayer(PlayerManager.Instance.PlayerPosOnStop, PlayerManager.Instance.CurrentPlayerHp);
            }
        }

        UIManager.Instance.TimerText.SetActive(true);
        UIManager.Instance.HPUI.SetActive(true);
        _settingStatue.SetActive(false);
        gameObject.SetActive(false);
    }
    private void ToggleChildren()
    {
        toggled = !toggled;
        gameObject.transform.GetChild(1).gameObject.SetActive(toggled);
        gameObject.transform.GetChild(2).gameObject.SetActive(toggled);
    }
}
