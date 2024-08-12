using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public GameObject FireBall, Center;
    [SerializeField]
    private float _timeBetweenFireBall;
    private float _initiationTime;

    private void Update()
    {
        InitializeFireBall();
    }

    private void InitializeFireBall()
    {
        if(_timeBetweenFireBall +  _initiationTime < Time.time)
        {
            GameObject.Instantiate(FireBall, Center.transform.position, Center.transform.rotation);
            _initiationTime = Time.time;
        }
    }
}
