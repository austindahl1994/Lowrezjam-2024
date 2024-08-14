using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Transform> checkpoints = new();
    public Vector2 latestCheckpointPosition;
    internal bool GameEnded = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateCheckpoint(Vector2 pos) {
        if (pos.y > latestCheckpointPosition.y) { 
            latestCheckpointPosition = pos;
        }
    }

    public void LightTorches() {
        if (checkpoints.Count == 0) {
            return;
        }
        foreach (Transform t in checkpoints) { 
            t.GetComponent<Checkpoint>().lit = true;
            t.GetComponent<Animator>().SetBool("Lit", true);
        }
    }

    public void RespawnOnCheckpoint() {
        if (latestCheckpointPosition == Vector2.zero || latestCheckpointPosition == new Vector2(0.5f, -1.5f)) {
            RespawnAtStart();
            return;
        }
        UIManager.Instance.GetComponent<Timer>().AddTime();
        UIManager.Instance.RaiseCurtains();
        PlayerManager.Instance.InitializePlayer(latestCheckpointPosition);
    }

    public void RespawnAtStart() {
        foreach (Transform cp in checkpoints)
        {
            if (cp != null) { 
                cp.gameObject.GetComponent<Checkpoint>().ResetCheckpoint();
            }
        }
        checkpoints.Clear();
        latestCheckpointPosition = new Vector2(0.5f, -1.5f);
        UIManager.Instance.GetComponent<Timer>().RestartTimer();
        UIManager.Instance.RaiseCurtains();
        PlayerManager.Instance.ResetDeathCount();
        PlayerManager.Instance.InitializePlayer(latestCheckpointPosition);
    }
}
