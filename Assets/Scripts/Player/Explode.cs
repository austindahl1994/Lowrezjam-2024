using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField]
    private Transform[] bodyParts;

    private readonly float force = 5f;

    private void Start()
    {
        foreach (Transform part in bodyParts) {
            if (part.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                // Generate a random direction
                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                // Apply force in the random direction
                rb.AddForce(randomDirection * force, ForceMode2D.Impulse);
            }
        }
    }
}
