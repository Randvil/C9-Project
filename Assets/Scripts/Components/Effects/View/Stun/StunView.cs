using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunView : IStunVeiw
{
    private Animator animator;

    public StunView(Animator animator)
    {
        this.animator = animator;
    }

    public void StartStun()
    {
        animator.SetBool("IsStunned", true);
    }

    public void BreakStun()
    {
        animator.SetBool("IsStunned", false);
    }

}
