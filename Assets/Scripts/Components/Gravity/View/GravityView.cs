using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityView
{
    private IGravity gravity;
    private Animator animator;

    public GravityView (IGravity gravity, Animator animator)
    {
        this.gravity = gravity;
        this.animator = animator;

        gravity.StartFallEvent.AddListener(OnStartFall);
        gravity.BreakFallEvent.AddListener(OnBreakFall);
        gravity.GroundedEvent.AddListener(OnGrounded);
    }

    public void OnStartFall()
    {
        animator.SetBool("IsFalling", true);
    }

    public void OnBreakFall()
    {
        animator.SetBool("IsFalling", false);
    }

    public void OnGrounded()
    {
        
    }
}
