using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathView : IDeathView
{
    private Animator animator;

    public DeathView(Animator animator)
    {
        this.animator = animator;
    }

    public void StartDying()
    {
        animator.SetBool("IsDying", true);
    }

    public void BreakDying()
    {
        animator.SetBool("IsDying", false);
    }

}
