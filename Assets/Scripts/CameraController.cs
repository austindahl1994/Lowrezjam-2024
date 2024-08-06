using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float CameraGap = 5f;
    public float Min = -545f, Max = 23.7f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(Player.transform.position.y - CameraGap, Min, Max), transform.position.z);
    }
}
