using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Parry : MonoBehaviour, IParry
{
    [Header("Stance")]

    [SerializeField, Range(0f, 5f), Tooltip("Maximum stance duration, [seconds]")]
    private float duration = 0.5f;

    [SerializeField, Range(0f, 10f), Tooltip("How often the player can stance [seconds]")]
    private float cooldown = 2f;


    [Header("Absorption")]

    [SerializeField, Tooltip("Whether the damage should be absorbed in units or in parts of the incoming damage")]
    private eParryType parryType = eParryType.RelativeAbsorption;

    [SerializeField, Min(0f), Tooltip("Absorption value [units or parts of the incoming damage depending on parry type]")]
    private float damageAbsorption = 1f;


    [Header("Reflection")]

    [SerializeField, Tooltip("Should melee damage be reflected?")]
    private bool reflectMeleeDamage;

    [SerializeField, Tooltip("Should projectiles be reflected?")]
    private bool reflectProjectiles;

    [SerializeField, Range(0f, 10f), Tooltip("How much damage should be reflected? [parts of the incoming damage]")]
    private float reflectionDamageMultiplier = 1f;


    [Header("Attack buff")]

    [SerializeField, Tooltip("Should the damage of the following attacks be increased after successful melee parry?")]
    private bool meleeParryAmplifyDamage = true;

    [SerializeField, Tooltip("Should the damage of the following attacks be increased after successful range parry?")]
    private bool rangeParryAmplifyDamage = false;

    [SerializeField, Range(0f, 5f), Tooltip("How much extra damage your weapon should deal with the following attack? [parts of weapon damage]")]
    private float extraDamage = 1f;

    [SerializeField, Range(1, 10), Tooltip("How many attacks should be amplified?")]
    private int attackNumber = 1;

    [SerializeField, Range(0f, 10f), Tooltip("How long the attack buff will last? [seconds]")]
    private float amplifyDuration = 3f;


    private Coroutine parryCoroutine;
    private float finishCooldownTime;
    private IDamageModificator absorption;
    private IDamageEffect meleeDamageReflection;
    private IDamageEffect projectileReflection;
    private IDamageModificator damageAmplification;
    private Coroutine amplifyDamageCoroutine;
    private int attackCounter;


    private IDamageHandler damageHandler;
    private ITurning turning;
    private ITeam team;
    private IWeapon weapon;


    public bool IsOnCooldown { get => Time.time < finishCooldownTime; }
    public bool IsParrying { get => parryCoroutine != null; }


    public UnityEvent StartParryEvent { get; } = new();
    public UnityEvent StopParryEvent { get; } = new();


    public float Duration { get => duration; set => duration = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public eParryType ParryType { get => parryType; set => parryType = value; }
    public float DamageAbsorption { get => damageAbsorption; set => damageAbsorption = value; }
    public bool ReflectMeleeDamage { get => reflectMeleeDamage; set => reflectMeleeDamage = value; }
    public bool ReflectProjectiles { get => reflectProjectiles; set => reflectProjectiles = value; }
    public float ReflectionDamageMultiplier { get => reflectionDamageMultiplier; set => reflectionDamageMultiplier = value; }
    public bool MeleeParryAmplifyDamage { get => meleeParryAmplifyDamage; set => meleeParryAmplifyDamage = value; }
    public bool RangeParryAmplifyDamage { get => rangeParryAmplifyDamage; set => rangeParryAmplifyDamage = value; }
    public float DamageBultiplier { get => extraDamage; set => extraDamage = value; }
    public int AttackNumber { get => attackNumber; set => attackNumber = value; }


    private void Start()
    {
        damageHandler = GetComponent<IDamageHandler>();
        turning = GetComponent<ITurning>();
        team = GetComponent<ITeam>();
        weapon = GetComponent<IWeapon>();
    }

    public void StartParry(eDirection direction)
    {
        if (parryCoroutine == null && !IsOnCooldown)
            parryCoroutine = StartCoroutine(ParryCoroutine());
    }

    public void StopParry()
    {
        if (parryCoroutine != null)
        {
            StopCoroutine(parryCoroutine);
            FinishParry();
        }
    }

    public IEnumerator ParryCoroutine()
    { 
        SetParryConditions();

        yield return new WaitForSeconds(duration);

        FinishParry();
    }

    private void SetParryConditions()
    {
        StartParryEvent.Invoke();

        // absorptions
        switch (parryType)
        {
            case eParryType.AbsoluteAbsorption:
                absorption = new AbsoluteDamageModificator(-damageAbsorption);
                break;

            case eParryType.RelativeAbsorption:
                absorption = new RelativeDamageModificator(-damageAbsorption);
                break;
        }
        damageHandler.AddDamageModificator(absorption);

        // reflections
        if (reflectMeleeDamage)
        {
            meleeDamageReflection = new ParryMeleeDamageReflection(gameObject, turning);
            damageHandler.AddDamageEffect(meleeDamageReflection);
            if (meleeDamageReflection is IParryDamageEffect parryDamageEffect)
                parryDamageEffect.SuccessfulParryEvent.AddListener(OnSuccessfulParry);
        }

        if (reflectProjectiles)
        {
            projectileReflection = new ParryProjectileReflection(gameObject, turning, team);
            damageHandler.AddDamageEffect(projectileReflection);
            if (projectileReflection is IParryDamageEffect parryDamageEffect)
                parryDamageEffect.SuccessfulParryEvent.AddListener(OnSuccessfulParry);
        }            
    }

    private void FinishParry()
    {
        damageHandler.RemoveDamageModificator(absorption);
        damageHandler.RemoveDamageEffect(meleeDamageReflection);
        damageHandler.RemoveDamageEffect(projectileReflection);

        finishCooldownTime = Time.time + cooldown;
        StopParryEvent.Invoke();
        parryCoroutine = null;
    }

    private IEnumerator AmplifyDamageCoroutine()
    {
        // add amplification
        damageAmplification = new RelativeDamageModificator(extraDamage);
        weapon.Damage.modificators.Add(damageAmplification);
        weapon.ReleaseAttackEvent.AddListener(OnReleaseAttack);
        attackCounter = 0;

        yield return new WaitForSeconds(amplifyDuration);

        RemoveAmplification();
    }

    private void RemoveAmplification()
    {
        weapon.Damage.modificators.Remove(damageAmplification);
        weapon.ReleaseAttackEvent.RemoveListener(OnReleaseAttack);

        amplifyDamageCoroutine = null;
    }

    private void OnSuccessfulParry()
    {
        if (amplifyDamageCoroutine != null)
            return;

        if (damageAmplification is MeleeDamageReflection && meleeParryAmplifyDamage || 
            damageAmplification is ProjectileReflection && rangeParryAmplifyDamage)
        {
            amplifyDamageCoroutine = StartCoroutine(AmplifyDamageCoroutine());
        }        
    }

    private void OnReleaseAttack()
    {
        if (attackCounter >= attackNumber)
        {
            StopCoroutine(amplifyDamageCoroutine);
            RemoveAmplification();
        }
    }
}
