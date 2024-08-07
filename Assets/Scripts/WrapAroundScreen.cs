using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAroundScreen : MonoBehaviour
{
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        float rightBound = 3.63f;
        float leftBound = -3.63f;

        if (transform.position.x >= rightBound && _rb.velocity.x > 0)
        {
            transform.position = new Vector2(leftBound, transform.position.y);
        }
        else if (transform.position.x <= leftBound && _rb.velocity.x < 0)
        {
            transform.position = new Vector2(rightBound, transform.position.y);
        }
    }
}
