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
        animator.SetFloat("JumpBlend", 0.5f);
        //animator.SetFloat("JumpBlend", 0, 0.1f, 1f);
    }

    public void BreakJump()
    {
        animator.SetBool("IsJumping", false);
        //animator.SetFloat("JumpBlend", 1, 0.5f, Time.deltaTime * 15f);
        //animator.SetFloat("JumpBlend", 0);
    }

    public void UpdateJumpParams()
    {
        animator.SetFloat("JumpBlend", 1f, jump.JumpTime, 5f * Time.deltaTime);
    }
}
