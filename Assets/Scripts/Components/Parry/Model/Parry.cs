using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Parry : IParry, IDamageDealer
{
    private MonoBehaviour owner;
    private GameObject character;

    private float duration;
    private float cooldown;
    private float damageAbsorption;
    private bool reflectMeleeDamage;
    private bool reflectProjectiles;
    private float reflectionDamageMultiplier;
    private bool meleeAmplifyDamage;
    private bool rangeAmplifyDamage;
    private float extraAttackDamage;
    private int amplifiedAttackNumber;
    private float amplifyDuration;

    private ITurning turning;
    private ITeam team;
    private IDamageHandler damageHandler;
    private IWeapon weapon;
    private IModifierManager defenceModifierManager;
    private IModifierManager weaponModifierManager;
    private IEffectManager effectManager;

    private Coroutine parryCoroutine;
    private float finishCooldownTime;
    private IDamageModifier absorption;
    private IDamageEffect meleeDamageReflection;
    private IDamageEffect projectileReflection;
    private IDamageModifier damageAmplification;
    private Coroutine amplifyDamageCoroutine;
    private int attackCounter;

    public bool IsParrying { get => parryCoroutine != null; }
    public bool IsOnCooldown { get => Time.time < finishCooldownTime; }
    public bool CanParry => IsParrying == false && IsOnCooldown == false;
    public float Cooldown => cooldown;

    public UnityEvent StartParryEvent { get; } = new();
    public UnityEvent BreakParryEvent { get; } = new();
    public UnityEvent SuccessfulParryEvent { get; } = new();
    public UnityEvent<DamageInfo> DealDamageEventCallback { get; } = new();

    public Parry(MonoBehaviour owner, GameObject character, ParryData parryData, ITurning turning, ITeam team, IDamageHandler damageHandler, IWeapon weapon, IModifierManager defenceModifierManager, IModifierManager weaponModifierManager, IEffectManager effectManager)
    {
        this.owner = owner;
        this.character = character;

        duration = parryData.duration;
        cooldown = parryData.cooldown;
        damageAbsorption = parryData.damageAbsorption;
        reflectMeleeDamage = parryData.reflectMeleeDamage;
        reflectProjectiles = parryData.reflectProjectiles;
        reflectionDamageMultiplier = parryData.reflectionDamageMultiplier;
        meleeAmplifyDamage = parryData.meleeAmplifyDamage;
        rangeAmplifyDamage = parryData.rangeAmplifyDamage;
        extraAttackDamage = parryData.extraAttackDamage;
        amplifiedAttackNumber = parryData.amplifiedAttackNumber;
        amplifyDuration = parryData.amplifyDuration;

        this.turning = turning;
        this.team = team;
        this.damageHandler = damageHandler;
        this.weapon = weapon;
        this.defenceModifierManager = defenceModifierManager;
        this.weaponModifierManager = weaponModifierManager;
        this.effectManager = effectManager;
    }

    public void StartParry()
    {
        if (parryCoroutine != null)
        {
            return;
        }

        parryCoroutine = owner.StartCoroutine(ParryCoroutine());

        StartParryEvent.Invoke();
    }

    public void BreakParry()
    {
        if (IsParrying == true)
        {
            owner.StopCoroutine(parryCoroutine);
            parryCoroutine = null;

            defenceModifierManager.RemoveModifier(absorption);

            if (meleeDamageReflection != null)
            {
                effectManager.RemoveEffect(meleeDamageReflection);
            }
            if (projectileReflection != null)
            {
                effectManager.RemoveEffect(projectileReflection);
            }            

            damageHandler.TakeDamageEvent.RemoveListener(OnSuccessfulParry);

            finishCooldownTime = Time.time + cooldown;

            BreakParryEvent.Invoke();
        }
    }

    private IEnumerator ParryCoroutine()
    { 
        SetParryConditions();

        yield return new WaitForSeconds(duration);

        BreakParry();
    }

    private void SetParryConditions()
    {
        absorption = new RelativeDamageModifier(-damageAbsorption);
        defenceModifierManager.AddModifier(absorption);

        meleeDamageReflection = null;
        if (reflectMeleeDamage)
        {
            meleeDamageReflection = new ParryMeleeDamageReflection(float.MaxValue, character, turning, this);
            effectManager.AddEffect(meleeDamageReflection);
        }

        projectileReflection = null;
        if (reflectProjectiles)
        {
            projectileReflection = new ParryProjectileReflection(team, float.MaxValue, character, turning);
            effectManager.AddEffect(projectileReflection);
        }

        damageHandler.TakeDamageEvent.AddListener(OnSuccessfulParry);
    }


    private void OnSuccessfulParry(DamageInfo damageInfo)
    {
        SuccessfulParryEvent.Invoke();

        switch (damageInfo.damageType)
        {
            case eDamageType.MeleeWeapon:
                if (meleeAmplifyDamage == false || amplifyDamageCoroutine != null)
                {
                    return;
                }
                break;

            case eDamageType.RangedWeapon:
                if (rangeAmplifyDamage == false || amplifyDamageCoroutine != null)
                {
                    return;
                }
                break;
        }

        amplifyDamageCoroutine = owner.StartCoroutine(AmplifyDamageCoroutine());
    }

    private IEnumerator AmplifyDamageCoroutine()
    {
        damageAmplification = new RelativeDamageModifier(extraAttackDamage);
        weaponModifierManager.AddModifier(damageAmplification);
        weapon.ReleaseAttackEvent.AddListener(OnReleaseAttack);
        attackCounter = 0;

        yield return new WaitForSeconds(amplifyDuration);

        RemoveAmplification();
    }

    private void OnReleaseAttack()
    {
        attackCounter++;
        if (attackCounter >= amplifiedAttackNumber)
        {
            RemoveAmplification();
        }
    }

    private void RemoveAmplification()
    {
        if (amplifyDamageCoroutine == null)
        {
            return;
        }

        weaponModifierManager.RemoveModifier(damageAmplification);
        weapon.ReleaseAttackEvent.RemoveListener(OnReleaseAttack);

        owner.StopCoroutine(amplifyDamageCoroutine);
        amplifyDamageCoroutine = null;
    }
}
