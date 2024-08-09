using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform normalPlayer;
    private Transform explodingPlayer;

    [SerializeField]
    private CameraController cameraController;

    private void Start()
    {
        normalPlayer = gameObject.transform.GetChild(0);
        explodingPlayer = gameObject.transform.GetChild(1);
    }

    public void FunKill() {
        explodingPlayer.position = normalPlayer.position;
        normalPlayer.gameObject.SetActive(false);
        explodingPlayer.gameObject.SetActive(true);
        cameraController.ChangeTarget(1);
    }
}
