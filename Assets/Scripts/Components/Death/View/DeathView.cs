using UnityEngine;

public class DeathView
{
    private string animatorParameter;
    private AudioClip deathAudioClip;

    private Animator animator;
    private AudioSource audioSource;

    public DeathView(DeathViewData deathViewData, IDeathManager deathManager, Animator animator, AudioSource audioSource)
    {
        animatorParameter = deathViewData.animatorParameter; 
        deathAudioClip = deathViewData.deathAudioClip;

        this.animator = animator;
        this.audioSource = audioSource;

        deathManager.DeathEvent.AddListener(OnDeath);
        deathManager.ResurrectionEvent.AddListener(OnResurrect);
    }

    public void OnDeath()
    {
        SetAnimatorParameter(true);
        PlayDeathSound();
    }

    public void OnResurrect()
    {
        SetAnimatorParameter(false);
    }

    private void SetAnimatorParameter(bool value)
    {
        animator.SetBool(animatorParameter, value);
    }

    private void PlayDeathSound()
    {
        if (deathAudioClip != null)
        {
            audioSource.PlayOneShot(deathAudioClip);
        }
    }
}
