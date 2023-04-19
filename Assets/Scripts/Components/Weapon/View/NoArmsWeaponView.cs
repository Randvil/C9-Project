using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoArmsWeaponView : IWeaponView
{
    private IWeapon weapon;
    private Animator animator;
    private AudioSource audioSource;

    public NoArmsWeaponView(IWeapon weapon, Animator animator, AudioSource audioSource)
    {
        this.weapon = weapon;
        this.animator = animator;
        this.audioSource = audioSource;

        weapon.ReleaseAttackEvent.AddListener(ReleaseAttack);
    }

    public void StartAttack()
    {
        animator.SetBool("IsAttacking", true);
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);

        audioSource.Play();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }

    public void BreakAttack()
    {
        animator.SetBool("IsAttacking", false);
    }

    public void ReleaseAttack()
    {

    }
}
