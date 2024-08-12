using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public GameObject player;

    [SerializeField]
    private Transform bodyParent;
    public int PlayerMaxHP { get; private set; }
    public int CurrentPlayerHp { get; private set; }
    public bool PlayerDead { get; private set; }
    public int PlayerDeathCount { get; private set; }
    public Vector2 CurrentPlayerPosition { get; private set; }

    internal Vector2 PlayerPosOnStop;
    internal bool CanLowerHP = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Start()
    {
        PlayerDeathCount = 0;
        InitializePlayer(new Vector2(0.5f, -1.5f));
    }

    public void InitializePlayer(Vector2 pos, int hp = 3) {
        UIManager.Instance.GetComponent<Timer>().StartTimer();
        ResetMaxHp();
        if (PlayerDead) {
            player.SetActive(true);
            //player.GetComponent<PlayerState>().ResetState();
            PlayerDead = false;
            EnemyManager.Instance.ResetMimics();
        }
        if (bodyParent.GetChild(0).gameObject.activeInHierarchy) {
            bodyParent.GetComponentInChildren<PartResetter>().ResetParts();
        }
        CurrentPlayerHp = hp;
        ChangeUiHp(CurrentPlayerHp);
        player.GetComponent<Movement>().canMove = true;
        player.transform.position = new Vector2(pos.x, pos.y + 0.1f);
        bodyParent.GetChild(0).transform.position = player.transform.position;
    }

    public void IncreaseMaxHp()
    {
        PlayerMaxHP++;
    }

    public void ResetMaxHp() {
        PlayerMaxHP = 3;
    }

    public void ChangeHp(int amount, bool killPlayer = false, bool fullHeal = false, int deathType = 0, float bloodDirection = 0) {
        if (PlayerDead) {
            return;
        }
        if (killPlayer || CurrentPlayerHp - amount <= 0) {
            UIManager.Instance.GetComponent<Timer>().PauseTimer();
            UIManager.Instance.LowerCurtains();
            PlayerDeathCount++;
            player.GetComponent<Movement>().canMove = false;
            if (deathType == 0) {
                player.GetComponent<PlayerParticles>().Spurt(bloodDirection);
            }
            PlayerDead = true;
            ChangeUiHp(0);
            PlayPlayerDeathSound();

            DeathStyle(deathType);
            return;
        } else if (fullHeal && CurrentPlayerHp > 0) {
            CurrentPlayerHp = PlayerMaxHP;
            ChangeUiHp(PlayerMaxHP);
        } else {
            if(CanLowerHP)
                CurrentPlayerHp -= amount;
            player.GetComponent<PlayerParticles>().Spurt(bloodDirection);
            PlayPlayerDamageSound();
            ChangeUiHp(CurrentPlayerHp);
        }
    }

    public void ChangeUiHp(int setAmount) {
        UIManager.Instance.ChangeHpValue(setAmount);
    }

    public void PlayPlayerDamageSound() {
        SoundManager.Instance.PlayPlayerSfx("Damage Sounds");
    }

    public void PlayPlayerDeathSound()
    {
        SoundManager.Instance.PlayPlayerSfx("Death Sounds");
    }

    private void DeathStyle(int deathType) {
        switch (deathType)
        {
            case 1:
                player.GetComponentInParent<PlayerController>().FunKill();
                break;
            default:
                //Debug.Log("Died normally, setting state to 3");
                //player.GetComponent<PlayerState>().SetState(3);
                break;
        }
    }
}
