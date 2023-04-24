using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpView
{
    private AudioClip jumpAudioClip;

    private IJump jump;
    private Animator animator;
    private AudioSource audioSource;

    public JumpView(JumpViewData jumpViewData, IJump jump, Animator animator, AudioSource audioSource)
    {
        jumpAudioClip = jumpViewData.jumpAudioClip;

        this.jump = jump;
        this.animator = animator;
        this.audioSource = audioSource;

        jump.StartJumpEvent.AddListener(OnStartJump);
        jump.BreakJumpEvent.AddListener(OnBreakJump);
    }

    public void OnStartJump()
    {
        animator.SetBool("IsJumping", true);
        animator.SetTrigger("JumpTrigger");
        audioSource.PlayOneShot(jumpAudioClip);
    }

    public void OnBreakJump()
    {
        animator.SetBool("IsJumping", false);
    }
}
