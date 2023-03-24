using UnityEngine;
using System.Collections;

public class MeleeWeaponView : IWeaponView
{
    private GameObject weaponObject;
    private Transform weaponContainer;
    private Transform rightHand;
    private IAnimatedWeapon weapon;
    private Animator animator;

    public MeleeWeaponView(GameObject weaponObject, Transform weaponContainer, Transform rightHand, IAnimatedWeapon weapon, Animator animator)
    {
        this.weaponObject = weaponObject;
        this.weaponContainer = weaponContainer;
        this.rightHand = rightHand;
        this.weapon = weapon;
        this.animator = animator;

        weapon.ReleaseAttackEvent.AddListener(ReleaseAttack);
    }

    public void StartAttack()
    {
        weaponObject.SetActive(true);
        weaponObject.transform.parent = rightHand;
        weaponObject.transform.position = rightHand.position;

        animator.SetBool("IsAttacking", true);
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);
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