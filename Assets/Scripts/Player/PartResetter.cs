using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartResetter : MonoBehaviour
{
    public void ResetParts()
    {
        foreach (Transform child in transform) { 
            child.transform.position = transform.position;
        }
        gameObject.SetActive(false);
    }
}

