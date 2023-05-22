using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoArmsWeaponView
{
    private IWeapon weapon;
    private Animator animator;
    private AudioSource audioSource;
    private AudioClip startAttackAudioClip;
    private AudioClip releaseAttackAudioClip;

    public NoArmsWeaponView(NoArmsWeaponViewData weaponViewData, IWeapon weapon, Animator animator, AudioSource audioSource)
    {
        this.weapon = weapon;
        this.animator = animator;
        this.audioSource = audioSource;
        startAttackAudioClip = weaponViewData.startAttackAudioClip;
        releaseAttackAudioClip = weaponViewData.releaseAttackAudioClip;

        weapon.StartAttackEvent.AddListener(OnStartAttack);
        weapon.BreakAttackEvent.AddListener(OnBreakAttack);
        weapon.ReleaseAttackEvent.AddListener(OnReleaseAttack);
    }

    public void OnStartAttack()
    {
        animator.SetTrigger("AttackTrigger");
        animator.SetBool("IsAttacking", true);
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);

        if (startAttackAudioClip != null)
        {
            audioSource.PlayOneShot(startAttackAudioClip);
        }
    }

    public void OnBreakAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    public void OnReleaseAttack()
    {
        if (releaseAttackAudioClip != null)
        {
            audioSource.PlayOneShot(releaseAttackAudioClip);
        }
    }
}
