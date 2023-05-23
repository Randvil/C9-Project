using System.Collections;
using UnityEngine;

public class DeathView
{
    private MonoBehaviour owner;

    private string animatorParameter;
    private AudioClip deathAudioClip;

    private Animator animator;
    private AudioSource audioSource;

    public DeathView(MonoBehaviour owner, DeathViewData deathViewData, IDeathManager deathManager, Animator animator, AudioSource audioSource)
    {
        this.owner = owner;

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
        owner.StartCoroutine(PlayDeathSound());
    }

    public void OnResurrect()
    {
        SetAnimatorParameter(false);
    }

    private void SetAnimatorParameter(bool value)
    {
        animator.SetBool(animatorParameter, value);
    }

    private IEnumerator PlayDeathSound()
    {
        if (deathAudioClip != null)
        {
            yield return new WaitUntil(() => audioSource.isPlaying == false);
            audioSource.PlayOneShot(deathAudioClip);
        }
    }
}
