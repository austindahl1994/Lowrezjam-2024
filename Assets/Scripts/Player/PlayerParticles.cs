using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem blood;

    public void Spurt(float direction) {
        //Debug.Log($"Direction of {direction}");
        var shape = blood.shape;
        Vector3 newRotation = shape.rotation;

        newRotation.x = direction;

        shape.rotation = newRotation;

        blood.Play();
    }
}
