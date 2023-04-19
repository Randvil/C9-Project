using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Parry : IParry, IDamageDealer
{
    private GameObject character;

    private ParryData parryData;

    private Coroutine parryCoroutine;
    private float finishCooldownTime;
    private IDamageModifier absorption;
    private IDamageEffect meleeDamageReflection;
    private IDamageEffect projectileReflection;
    private IDamageModifier damageAmplification;
    private Coroutine amplifyDamageCoroutine;
    private int attackCounter;

    private ITurning turning;
    private ITeam team;
    private IDamageHandler damageHandler;
    private IWeapon weapon;
    private IModifierManager defenceModifierManager;
    private IModifierManager weaponModifierManager;
    private IEffectManager effectManager;

    public bool IsParrying { get => parryCoroutine != null; }
    public bool IsOnCooldown { get => Time.time < finishCooldownTime; }
    public bool CanParry => IsParrying == false && IsOnCooldown == false;

    public UnityEvent ParryEvent { get; } = new();
    public UnityEvent<DamageInfo> DealDamageEvent { get; } = new();

    public Parry(GameObject character, ParryData parryData, ITurning turning, ITeam team, IDamageHandler damageHandler, IWeapon weapon, IModifierManager defenceModifierManager, IModifierManager weaponModifierManager, IEffectManager effectManager)
    {
        this.character = character;

        this.parryData = parryData;

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
        parryCoroutine = Coroutines.StartCoroutine(ParryCoroutine());
    }

    public void BreakParry()
    {
        if (IsParrying == true)
        {
            Coroutines.StopCoroutine(ref parryCoroutine);

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

            finishCooldownTime = Time.time + parryData.cooldown;
        }
    }

    private IEnumerator ParryCoroutine()
    { 
        SetParryConditions();

        yield return new WaitForSeconds(parryData.duration);

        BreakParry();
    }

    private void SetParryConditions()
    {
        absorption = new RelativeDamageModifier(-parryData.damageAbsorption);
        defenceModifierManager.AddModifier(absorption);

        meleeDamageReflection = null;
        if (parryData.reflectMeleeDamage)
        {
            meleeDamageReflection = new ParryMeleeDamageReflection(float.MaxValue, character, turning, this);
            effectManager.AddEffect(meleeDamageReflection);
        }

        projectileReflection = null;
        if (parryData.reflectProjectiles)
        {
            projectileReflection = new ParryProjectileReflection(team, float.MaxValue, character, turning);
            effectManager.AddEffect(projectileReflection);
        }

        damageHandler.TakeDamageEvent.AddListener(OnSuccessfulParry);
    }


    private void OnSuccessfulParry(DamageInfo damageInfo)
    {
        ParryEvent.Invoke();

        switch (damageInfo.damageType)
        {
            case eDamageType.MeleeWeapon:
                if (parryData.meleeAmplifyDamage == false || amplifyDamageCoroutine != null)
                {
                    return;
                }
                break;

            case eDamageType.RangedWeapon:
                if (parryData.rangeAmplifyDamage == false || amplifyDamageCoroutine != null)
                {
                    return;
                }
                break;
        }

        amplifyDamageCoroutine = Coroutines.StartCoroutine(AmplifyDamageCoroutine());
    }

    private IEnumerator AmplifyDamageCoroutine()
    {
        damageAmplification = new RelativeDamageModifier(parryData.extraAttackDamage);
        weaponModifierManager.AddModifier(damageAmplification);
        weapon.ReleaseAttackEvent.AddListener(OnReleaseAttack);
        attackCounter = 0;

        yield return new WaitForSeconds(parryData.amplifyDuration);

        RemoveAmplification();
    }

    private void OnReleaseAttack()
    {
        attackCounter++;
        if (attackCounter >= parryData.amplifiedAttackNumber)
        {
            Coroutines.StopCoroutine(ref amplifyDamageCoroutine);
            RemoveAmplification();
        }
    }

    private void RemoveAmplification()
    {
        weaponModifierManager.RemoveModifier(damageAmplification);
        weapon.ReleaseAttackEvent.RemoveListener(OnReleaseAttack);
    }
}
