using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charon : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private Transform _target;

    public void MoveCharon()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }
}
