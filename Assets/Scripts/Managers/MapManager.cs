using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public HashSet<Vector2> checkVectors = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddToVectorList(Vector2 v, int x, int y) {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector2 positionToAdd = v + new Vector2(i, j);
                checkVectors.Add(positionToAdd);
            }
        }
    }

    public bool CheckVectorList(Vector2 pos) {
        //Debug.Log($"Checking Hashset for {pos} closest to {RoundToNearestHalf(pos)}");
        //Debug.Log($"Hashset has the Vector2: {checkVectors.Contains(RoundToNearestHalf(pos))} and contains {checkVectors.Count}");
        return checkVectors.Contains(RoundToNearestHalf(pos));
    }

    private Vector2 RoundToNearestHalf(Vector2 vector)
    {
        // Round each component to the nearest 0.5
        float x = RoundToNearestHalf(vector.x);
        float y = RoundToNearestHalf(vector.y);
        if (Mathf.Abs(x) > 3.5) {
            x = (float)(x > 0 ? 3.5 : -3.5);
        }
        return new Vector2(x, y);
    }

    private float RoundToNearestHalf(float value)
    {
        return Mathf.Round(value * 2) / 2;
    }
}
