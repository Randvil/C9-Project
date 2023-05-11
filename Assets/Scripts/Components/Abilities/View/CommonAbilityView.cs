using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAbilityView
{
    private string animatorParameter;
    private AudioClip startCastSoundEffect;
    private AudioClip releaseCastSoundEffect;
    private AudioClip breakCastSoundEffect;

    private Animator animator;
    private AudioSource audioSource;

    public CommonAbilityView(CommonAbilityViewData abilityViewData, IAbility ability, Animator animator, AudioSource audioSource)
    {
        animatorParameter = abilityViewData.animatorParameter;
        startCastSoundEffect = abilityViewData.startCastSoundEffect;
        releaseCastSoundEffect = abilityViewData.releaseCastSoundEffect;
        breakCastSoundEffect = abilityViewData.breakCastSoundEffect;

        this.animator = animator;
        this.audioSource = audioSource;

        ability.StartCastEvent.AddListener(OnStartCast);
        ability.ReleaseCastEvent.AddListener(OnReleaseCast);
        ability.BreakCastEvent.AddListener(OnBreakCast);
    }


    private void OnStartCast()
    {
        animator.SetBool(animatorParameter, true);
        PlaySoundEffect(startCastSoundEffect);
    }

    private void OnReleaseCast()
    {
        PlaySoundEffect(releaseCastSoundEffect);
    }

    private void OnBreakCast()
    {
        animator.SetBool(animatorParameter, false);
        PlaySoundEffect(breakCastSoundEffect);
    }

    private void PlaySoundEffect(AudioClip soundEffect)
    {
        if (soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}
