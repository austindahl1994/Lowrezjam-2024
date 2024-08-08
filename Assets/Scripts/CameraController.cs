using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerHead;
    private GameObject target;
    public float CameraGap = 5f;
    public float Min = -545f, Max = 23.7f;
    private void Awake()
    {
        target = Player;
    }

    public void ChangeTarget() { 
        target = PlayerHead;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(target.transform.position.y - CameraGap, Min, Max), transform.position.z);
    }
}
