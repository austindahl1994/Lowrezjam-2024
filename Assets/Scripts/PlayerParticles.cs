using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem blood;
    
    public void Spurt(float direction) {
        var shape = blood.shape;
        Vector3 newRotation = shape.rotation;

        newRotation.y = direction == 0 ? 0 : (direction == 1 ? -30f : 30f);

        shape.rotation = newRotation;

        blood.Play();
    }
}
