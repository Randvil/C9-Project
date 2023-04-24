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

    protected Coroutine strikeCoroutine;
    protected float finishCooldownTime;
    protected float startCastTime;

    protected IEnergyManager energyManager;

    public virtual bool IsPerforming { get => strikeCoroutine != null; }
    public virtual bool IsOnCooldown { get => Time.time < finishCooldownTime; }
    public virtual bool CanBeUsed => IsPerforming == false && IsOnCooldown == false && energyManager.Energy.currentEnergy >= cost;

    public UnityEvent StartCastEvent { get; } = new();
    public UnityEvent BreakCastEvent { get; } = new();
    public UnityEvent ReleaseCastEvent { get; } = new();

    public AbstractAbility(MonoBehaviour owner, BaseAbilityData baseAbilityData, IEnergyManager energyManager)
    {
        this.owner = owner;

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
            strikeCoroutine = owner.StartCoroutine(ReleaseStrikeCoroutine());

            StartCastEvent.Invoke();
        }
    }

    public virtual void BreakCast()
    {
        if (IsPerforming == true)
        {
            owner.StopCoroutine(strikeCoroutine);
            strikeCoroutine = null;

            BreakCastEvent.Invoke();
        }
    }

    protected abstract IEnumerator ReleaseStrikeCoroutine();
}
