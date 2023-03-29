using UnityEngine;
using System.Collections;

public class PlayerWeaponView : IWeaponView
{
    private GameObject weaponObject;
    private Transform weaponContainer;
    private Transform rightHand;

    private IWeapon weapon;
    private Animator animator;
    private AudioSource audioSource;

    private Quaternion weaponRotation;

    public PlayerWeaponView(GameObject weaponObject, Transform weaponContainer, Transform rightHand, IWeapon weapon, Animator animator, AudioSource audioSource)
    {
        this.weaponObject = weaponObject;
        this.weaponContainer = weaponContainer;
        this.rightHand = rightHand;

        this.weapon = weapon;
        this.animator = animator;
        this.audioSource = audioSource;

        weaponRotation = weaponObject.transform.rotation;

        weapon.ReleaseAttackEvent.AddListener(ReleaseAttack);
    }

    public void StartAttack()
    {
        weaponObject.SetActive(true);
        weaponObject.transform.parent = rightHand;
        weaponObject.transform.position = rightHand.position;
        weaponObject.transform.localRotation = Quaternion.identity;

        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("AttackTrigger");
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);

        audioSource.Play();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }

    public void BreakAttack()
    {
        weaponObject.SetActive(false);
        weaponObject.transform.parent = weaponContainer;

        animator.SetBool("IsAttacking", false);
    }

    public void ReleaseAttack()
    {
        
    }
}