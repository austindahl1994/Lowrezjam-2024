using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //Idle is 0, walk 1, jump 2, die 3
    private Animator animator;

    public int currentState;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetState(int newState)
    {
        if (currentState == newState || currentState == 3) return;

        currentState = newState;
        animator.SetInteger("State", currentState);
    }
}
