using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> checkpoints = new();
    public static GameManager Instance;
    public Vector2 latestCheckpointPosition;
    public int HpAtCheckpoint;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateCheckpoint(Vector2 pos, int hp) {
        if (pos.y > latestCheckpointPosition.y) { 
            latestCheckpointPosition = pos;
            HpAtCheckpoint = hp;
        }
    }

    public void RespawnOnCheckpoint() {
        foreach (Transform cp in checkpoints) {
            if (cp.position.y < latestCheckpointPosition.y) {
                cp.gameObject.GetComponent<Checkpoint>().lit = false;
            }
        }
        PlayerManager.Instance.InitializePlayer(latestCheckpointPosition, HpAtCheckpoint);
    }

    //Keep HP upgrades? Input PlayerManager.Instance.PlayerMaxHp if so, reset otherwise
    public void RespawnAtStart() {
        foreach (Transform cp in checkpoints)
        {
            cp.gameObject.GetComponent<Checkpoint>().lit = false;
        }
        latestCheckpointPosition = new Vector2(0.5f, -1.5f);
        //Set to player max HP?
        HpAtCheckpoint = 3;
        PlayerManager.Instance.InitializePlayer(latestCheckpointPosition, HpAtCheckpoint);
    }
}
