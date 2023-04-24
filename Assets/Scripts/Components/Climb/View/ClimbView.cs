using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbView
{
    private IClimb climb;
    private Animator animator;

    public ClimbView(IClimb climb, Animator animator)
    {
        this.climb = climb;
        this.animator = animator;

        climb.StartClimbEvent.AddListener(StartClimb);
        climb.BreakClimbEvent.AddListener(BreakClimb);
        climb.ClimbStateChangedEvent.AddListener(OnChangeClimbState);
    }

    public void StartClimb()
    {
        animator.SetBool("IsClimbing", true);
        OnChangeClimbState();
    }

    public void BreakClimb()
    {
        animator.SetBool("IsClimbing", false);
        OnChangeClimbState();
    }

    public void OnChangeClimbState()
    {
        animator.SetFloat("ClimbSpeed", climb.ClimbSpeed);
    }

}
