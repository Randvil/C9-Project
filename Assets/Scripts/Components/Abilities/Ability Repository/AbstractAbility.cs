using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractAbility : IAbility
{
    protected MonoBehaviour owner;

    protected float cooldown;
    protected float preCastDelay;
    protected float postCastDelay;
    protected float cost;

    protected Coroutine abilityCoroutine;
    protected Coroutine strikeCoroutine;
    protected float finishCooldownTime;
    protected float startCastTime;

    protected IEnergyManager energyManager;

    public eAbilityType AbilityType { get; protected set; }
    public virtual bool IsPerforming => abilityCoroutine != null; 
    public virtual bool IsOnCooldown => Time.time < finishCooldownTime; 
    public virtual bool CanBeUsed => IsPerforming == false && IsOnCooldown == false && energyManager.Energy.currentEnergy >= cost;
    public float Cooldown => cooldown;

    public UnityEvent StartCastEvent { get; } = new();
    public UnityEvent BreakCastEvent { get; } = new();
    public UnityEvent ReleaseCastEvent { get; } = new();

    public AbstractAbility(MonoBehaviour owner, BaseAbilityData baseAbilityData, IEnergyManager energyManager)
    {
        this.owner = owner;

        AbilityType = baseAbilityData.abilityType;
        cooldown = baseAbilityData.cooldown;
        preCastDelay = baseAbilityData.preCastDelay;
        postCastDelay = baseAbilityData.postCastDelay;
        cost = baseAbilityData.cost;

        this.energyManager = energyManager;
    }

    public virtual void StartCast()
    {
        if (IsPerforming == false)
        {
            abilityCoroutine = owner.StartCoroutine(AbilityCoroutine());

            StartCastEvent.Invoke();
        }
    }

    public virtual void BreakCast()
    {
        if (IsPerforming == true)
        {
            owner.StopCoroutine(abilityCoroutine);
            abilityCoroutine = null;

            BreakCastEvent.Invoke();
        }
    }

    protected virtual IEnumerator AbilityCoroutine()
    {
        yield return new WaitForSeconds(preCastDelay);

        yield return ReleaseStrikeCoroutine();

        yield return new WaitForSeconds(postCastDelay);

        BreakCast();
    }

    protected abstract IEnumerator ReleaseStrikeCoroutine();
}
