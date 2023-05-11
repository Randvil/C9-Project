using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbView
{
    private IClimb climb;
    private Animator animator;
    private AudioSource audioSource;

    public ClimbView(IClimb climb, Animator animator, AudioSource audioSource)
    {
        this.climb = climb;
        this.animator = animator;
        this.audioSource = audioSource;

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
        float speed = climb.ClimbSpeed;

        if (Mathf.Abs(speed) > 0f)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        animator.SetFloat("ClimbSpeed", speed);
    }

}
