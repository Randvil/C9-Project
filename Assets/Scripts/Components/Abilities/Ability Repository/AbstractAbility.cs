using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractAbility : IAbility
{
    protected BaseAbilityData baseAbilityData;

    protected Coroutine strikeCoroutine;
    protected float finishCooldownTime;
    protected float startCastTime;

    protected IAbilityManager abilityManager;
    protected IEnergyManager energyManager;

    public eAbilityType Type { get; protected set; }
    public virtual bool IsPerforming => strikeCoroutine != null; 
    public virtual bool IsOnCooldown => Time.time < finishCooldownTime; 
    public virtual bool CanBeUsed => !IsPerforming && !IsOnCooldown && energyManager.Energy.currentEnergy >= baseAbilityData.cost;

    public UnityEvent ReleaseCastEvent { get; } = new();

    public AbstractAbility(BaseAbilityData baseAbilityData, IAbilityManager abilityManager, IEnergyManager energyManager)
    {
        this.baseAbilityData = baseAbilityData;

        this.abilityManager = abilityManager;
        this.energyManager = energyManager;
    }

    public virtual void StartCast()
    {
        strikeCoroutine = Coroutines.StartCoroutine(ReleaseStrikeCoroutine());
    }

    public virtual void BreakCast()
    {
        if (IsPerforming)
        {
            Coroutines.StopCoroutine(ref strikeCoroutine);
        }
    }

    protected abstract IEnumerator ReleaseStrikeCoroutine();
}
