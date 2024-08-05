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
        //Get the screen position of object in pixel
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(screenPos);
        //Get the right side of the screen in world unit
        float rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(2874f, Screen.height)).x;
        float leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(764f, 0f)).x;

        //if Player moving through left side of the screen
        if(screenPos.x <= 764f && _rb.velocity.x < 0)
        {
            transform.position = new Vector2(rightSideOfScreenInWorld, transform.position.y);
        }

        //if Player moving through right side of the screen
        else if (screenPos.x >= 3060f && _rb.velocity.x > 0)
        {
            transform.position = new Vector2(leftSideOfScreenInWorld, transform.position.y);
        }
    }
}
