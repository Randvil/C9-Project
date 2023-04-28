using UnityEngine;
using System.Collections;

public class PlayerWeaponView
{
    private GameObject weaponObject;
    private Transform weaponContainer;
    private Transform rightHand;

    private AudioClip takeSword;
    private AudioClip putAwaySword;
    private AudioClip hitEnemy;
    private AudioClip missEnemy;

    private IWeapon weapon;
    private IDamageDealer damageDealer;
    private Animator animator;
    private AudioSource audioSource;

    private bool enemyWasHit;

    public PlayerWeaponView(GameObject weaponObject, Transform weaponContainer, Transform rightHand, PlayerWeaponViewData playerWeaponViewData, IWeapon weapon, IDamageDealer damageDealer, Animator animator, AudioSource audioSource)
    {
        this.weaponObject = weaponObject;
        this.weaponContainer = weaponContainer;
        this.rightHand = rightHand;

        takeSword = playerWeaponViewData.takeSword;
        putAwaySword = playerWeaponViewData.putAwaySword;
        hitEnemy = playerWeaponViewData.hitEnemy;
        missEnemy = playerWeaponViewData.missEnemy;

        this.weapon = weapon;
        this.damageDealer = damageDealer;
        this.animator = animator;
        this.audioSource = audioSource;

        weapon.StartAttackEvent.AddListener(OnStartAttack);
        weapon.BreakAttackEvent.AddListener(OnBreakAttack);
        weapon.ReleaseAttackEvent.AddListener(OnReleaseAttack);
        damageDealer.DealDamageEvent.AddListener(OnDamageDeal);
    }

    public void OnStartAttack()
    {
        weaponObject.SetActive(true);
        weaponObject.transform.parent = rightHand;
        weaponObject.transform.position = rightHand.position;
        weaponObject.transform.localRotation = Quaternion.identity;

        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("AttackTrigger");
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);

        //audioSource.PlayOneShot(takeSword);
    }

    public void OnBreakAttack()
    {
        weaponObject.SetActive(false);
        weaponObject.transform.parent = weaponContainer;

        animator.SetBool("IsAttacking", false);

        //audioSource.PlayOneShot(putAwaySword);
    }

    public void OnReleaseAttack()
    {
        if (enemyWasHit == true)
        {
            audioSource.PlayOneShot(hitEnemy);
            enemyWasHit = false;
        }
        else
        {
            audioSource.PlayOneShot(missEnemy);
        }
    }

    public void OnDamageDeal(DamageInfo damageInfo)
    {
        enemyWasHit = true;
    }
}