using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpView : IJumpView
{
    private IJump jump;
    private Animator animator;

    public JumpView(IJump jump, Animator animator)
    {
        this.jump = jump;
        this.animator = animator;
    }

    public void StartJump()
    {
        animator.SetBool("IsJumping", true);
        animator.SetTrigger("JumpTrigger");
    }

    public void BreakJump()
    {
        animator.SetBool("IsJumping", false);
    }
}
