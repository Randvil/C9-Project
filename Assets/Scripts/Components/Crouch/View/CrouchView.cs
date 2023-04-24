using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchView
{
    private Animator animator;

    public CrouchView(ICrouch crouch, Animator animator)
    {
        this.animator = animator;

        crouch.StartCrouchEvent.AddListener(OnStartCrouch);
        crouch.BreakCrouchEvent.AddListener(OnBreakCrouch);
    }

    public void OnStartCrouch()
    {
        animator.SetBool("IsCrouching", true);
    }

    public void OnBreakCrouch()
    {
        animator.SetBool("IsCrouching", false);
    }
}
