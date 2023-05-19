using UnityEngine;
using UnityEngine.VFX;

public class PlayerWeaponView
{
    private GameObject weaponObject;
    private Transform weaponContainer;
    private Transform rightHand;

    private AudioClip takeSwordAudioClip;
    private AudioClip putAwaySwordAudioClip;
    private AudioClip[] hitAudioClips;
    private AudioClip missAudioClip;

    private IWeapon weapon;
    private IDamageDealer damageDealer;
    private Animator animator;
    private AudioSource audioSource;

    private VisualEffect slashGraph;

    private bool enemyWasHit;

    public PlayerWeaponView(GameObject weaponObject, PlayerWeaponViewData playerWeaponViewData, IWeapon weapon, IDamageDealer damageDealer, Animator animator, AudioSource audioSource, VisualEffect slashGraph)
    {
        this.weaponObject = weaponObject;
        //this.weaponContainer = weaponContainer;
        //this.rightHand = rightHand;

        takeSwordAudioClip = playerWeaponViewData.takeSwordAudioClip;
        putAwaySwordAudioClip = playerWeaponViewData.putAwaySwordAudioClip;
        hitAudioClips = playerWeaponViewData.hitAudioClips;
        missAudioClip = playerWeaponViewData.missAudioClip;

        this.weapon = weapon;
        this.damageDealer = damageDealer;
        this.animator = animator;
        this.audioSource = audioSource;

        weapon.StartAttackEvent.AddListener(OnStartAttack);
        weapon.BreakAttackEvent.AddListener(OnBreakAttack);
        weapon.ReleaseAttackEvent.AddListener(OnReleaseAttack);
        damageDealer.DealDamageEventCallback.AddListener(OnDamageDeal);

        this.slashGraph = slashGraph;
    }

    private void OnStartAttack()
    {
        weaponObject.SetActive(true);
        //weaponObject.transform.parent = rightHand;
        //weaponObject.transform.position = rightHand.position;
        //weaponObject.transform.localRotation = Quaternion.identity;

        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("AttackTrigger");
        animator.SetFloat("AttackSpeed", weapon.AttackSpeed);

        //PlaySound(takeSword);
    }

    private void OnBreakAttack()
    {
        weaponObject.SetActive(false);
        //weaponObject.transform.parent = weaponContainer;

        animator.SetBool("IsAttacking", false);

        //PlaySound(putAwaySword);
    }

    private void OnReleaseAttack()
    {
        SlashSwordVFXPlay();

        if (enemyWasHit == true)
        {
            AudioClip randomHitAudioClip = hitAudioClips[Random.Range(0, hitAudioClips.Length)];
            PlaySound(randomHitAudioClip);
            enemyWasHit = false;
        }
        else
        {
            PlaySound(missAudioClip);
        }
    }

    private void OnDamageDeal(DamageInfo damageInfo)
    {
        enemyWasHit = true;
    }

    public void SlashSwordVFXPlay()
    {
        switch (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {
            case "First Attack":
                slashGraph.SetFloat("Direction", -1f);
                break;
            case "Second Attack":
                slashGraph.SetFloat("Direction", 1f);
                break;
            case "Third Attack":
                slashGraph.SetFloat("Direction", 1f);
                break;
        }
        slashGraph.Play();
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}