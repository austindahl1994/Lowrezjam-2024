using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    public List<Transform> mimics = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ResetMimics() {
        foreach (Transform t in mimics) { 
            t.GetChild(0).gameObject.SetActive(true);
            t.GetChild(1).gameObject.SetActive(false);
        }
        mimics.Clear();
    }
}
