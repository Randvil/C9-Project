using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchView : ICrouchView
{
    private Animator animator;

    public CrouchView(Animator animator)
    {
        this.animator = animator;
    }

    public void StartCrouch()
    {
        animator.SetBool("IsCrouching", true);
    }

    public void BreakCrouch()
    {
        animator.SetBool("IsCrouching", false);
    }
}
