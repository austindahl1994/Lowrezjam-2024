using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> checkpoints = new();
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
        if (latestCheckpointPosition == Vector2.zero || latestCheckpointPosition == new Vector2(0.5f, -1.5f)) {
            RespawnAtStart();
            return;
        }
        foreach (Transform cp in checkpoints) {
            if (cp.position.y < latestCheckpointPosition.y) {
                cp.gameObject.GetComponent<Checkpoint>().lit = false;
            }
        }
        Camera.main.GetComponent<CameraController>().ChangeTarget(0);
        UIManager.Instance.RaiseCurtains();
        PlayerManager.Instance.InitializePlayer(latestCheckpointPosition, HpAtCheckpoint);
    }

    public void RespawnAtStart() {
        foreach (Transform cp in checkpoints)
        {
            if (cp != null) { 
                cp.gameObject.GetComponent<Checkpoint>().ResetCheckpoint();
            }
        }
        checkpoints.Clear();
        HpAtCheckpoint = 3;
        latestCheckpointPosition = new Vector2(0.5f, -1.5f);
        Camera.main.GetComponent<CameraController>().ChangeTarget(0);
        UIManager.Instance.RaiseCurtains();
        PlayerManager.Instance.InitializePlayer(latestCheckpointPosition);
    }
}
