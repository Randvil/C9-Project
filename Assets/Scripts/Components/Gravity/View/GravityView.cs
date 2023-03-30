using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityView : IGravityView
{
    private IGravity gravity;
    private Animator animator;

    public GravityView (IGravity gravity, Animator animator)
    {
        this.gravity = gravity;
        this.animator = animator;
    }

    public void SetFallingParams()
    {
        animator.SetBool("IsFalling", gravity.IsFalling);
    }
}
