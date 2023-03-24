using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpView : IJumpView
{
    private Animator animator;

    public JumpView(Animator animator)
    {
        this.animator = animator;
    }

    public void StartJump()
    {
        animator.SetTrigger("JumpTrigger");
        animator.SetBool("IsJumping", true);
    }

    public void BreakJump()
    {
        animator.SetBool("IsJumping", false);
    }
}
