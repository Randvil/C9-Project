using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityView
{
    private string animatorParameter;
    private AudioClip landingAudioClip;

    private IGravity gravity;
    private Animator animator;
    private AudioSource fallAudioSource;
    private AudioSource landingAudioSource;

    public GravityView (FallViewData fallViewData, IGravity gravity, Animator animator, AudioSource fallAudioSource, AudioSource landingAudioSource)
    {
        animatorParameter = fallViewData.animatorParameter;
        landingAudioClip = fallViewData.landingAudioClip;

        this.gravity = gravity;
        this.animator = animator;
        this.fallAudioSource = fallAudioSource;
        this.landingAudioSource = landingAudioSource;

        gravity.StartFallEvent.AddListener(OnStartFall);
        gravity.BreakFallEvent.AddListener(OnBreakFall);
        gravity.GroundedEvent.AddListener(OnGrounded);
    }

    public void OnStartFall()
    {
        animator.SetBool(animatorParameter, true);
        fallAudioSource.Play();
    }

    public void OnBreakFall()
    {
        animator.SetBool(animatorParameter, false);
        fallAudioSource.Stop();
    }

    public void OnGrounded()
    {
        landingAudioSource.PlayOneShot(landingAudioClip);
    }
}
