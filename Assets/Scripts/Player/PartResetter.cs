using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartResetter : MonoBehaviour
{
    private struct PartState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    private Dictionary<Transform, PartState> originalStates;

    public void SavePartsState() {
        SaveInitialStates();
    }

    private void SaveInitialStates()
    {
        originalStates = new Dictionary<Transform, PartState>();

        foreach (Transform part in transform.GetChild(0).transform)
        {
            PartState state = new()
            {
                position = part.localPosition,
                rotation = part.localRotation,
                scale = part.localScale
            };
            originalStates.Add(part, state);
        }
    }

    public void ResetParts()
    {
        foreach (KeyValuePair<Transform, PartState> entry in originalStates)
        {
            Transform part = entry.Key;
            PartState state = entry.Value;

            if (part != null && part.gameObject.activeInHierarchy)
            {
                part.SetLocalPositionAndRotation(state.position, state.rotation);
                part.localScale = state.scale;
            }
            part.gameObject.SetActive(false);
        }
    }
}

