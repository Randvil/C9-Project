using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ParryView
{
    private GameObject weaponObject;
    private Transform weaponContainer;
    private Transform rightHand;

    private string stanceAnimatorParameter;
    private string triggerAnimatorParameter;
    private AudioClip startParrySound;
    private AudioClip successfulParrySound;
    private AudioClip breakParrySound;

    private Animator animator;
    private AudioSource audioSource;
    private IParry parry;

    private VisualEffect parryEffect;
    private MonoBehaviour owner;

    public ParryView(GameObject weaponObject, ParryViewData parryViewData, IParry parry, Animator animator, AudioSource audioSource, VisualEffect visualEffect, MonoBehaviour owner)
    {
        this.weaponObject = weaponObject;
        //this.weaponContainer = weaponContainer;
        //this.rightHand = rightHand;

        stanceAnimatorParameter = parryViewData.stanceAnimatorParameter;
        triggerAnimatorParameter = parryViewData.triggerAnimatorParameter;
        startParrySound = parryViewData.startParrySound;
        successfulParrySound = parryViewData.successfulParrySound;
        breakParrySound = parryViewData.breakParrySound;

        this.animator = animator;
        this.audioSource = audioSource;
        this.parry = parry;
        parryEffect = visualEffect;
        this.owner = owner;

        parry.StartParryEvent.AddListener(OnStartParry);
        parry.BreakParryEvent.AddListener(OnBreakParry);
        parry.SuccessfulParryEvent.AddListener(OnSuccessfulParry);
        parry.AddDamageAmplificationEvent.AddListener(OnAddDamageAmplification);
        parry.RemoveDamageApmlificationEvent.AddListener(OnRemoveDamageAmplification);
    }

    private void OnStartParry()
    {
        weaponObject.SetActive(true);
        //weaponObject.transform.parent = rightHand;
        //weaponObject.transform.position = rightHand.position;
        //weaponObject.transform.localRotation = Quaternion.identity;

        animator.SetBool(stanceAnimatorParameter, true);

        PlaySound(startParrySound);
    }

    private void OnBreakParry()
    {
        weaponObject.SetActive(false);
        //weaponObject.transform.parent = weaponContainer;

        animator.SetBool(stanceAnimatorParameter, false);

        PlaySound(breakParrySound);
    }

    private void OnSuccessfulParry()
    {
        animator.SetTrigger(triggerAnimatorParameter);

        PlaySound(successfulParrySound);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void OnAddDamageAmplification()
    {
        parryEffect.Play();
    }

    private void OnRemoveDamageAmplification()
    {
        parryEffect.Stop();
    }
}
