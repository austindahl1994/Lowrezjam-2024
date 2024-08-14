using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerHead;
    public GameObject PlayerBody;
    private GameObject target;
    public float CameraGap = 5f;
    public float Min = -545f, Max = 23.7f;
    private void Awake()
    {
        target = Player;
    }

    public void ChangeTarget(int value)
    {
        target = value switch
        {
            1 => PlayerHead,
            2 => PlayerBody,
            _ => Player
        };
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(target.transform.position.y - CameraGap, Min, Max), transform.position.z);
    }
}
