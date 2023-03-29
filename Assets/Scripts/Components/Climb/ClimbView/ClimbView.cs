using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbView : IClimbView
{
    private Animator animator;
    private Rigidbody2D rigidbody;

    public ClimbView(Animator animator, Rigidbody2D rigidbody)
    {
        this.animator = animator;
        this.rigidbody = rigidbody;
    }

    public void StartClimb()
    {
        animator.SetBool("IsClimbing", true);
    }

    public void BreakClimb()
    {
        animator.SetBool("IsClimbing", false);
    }

    public void UpdateParameters()
    {
        animator.SetFloat("VerticalSpeed", rigidbody.velocity.y);
    }

}
