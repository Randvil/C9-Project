using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectManager : IEffectManager
{
    private MonoBehaviour owner;

    private List<IEffect> effects = new();
    private List<IStunEffect> stunEffects = new();
    private List<ISlowEffect> slowEffects = new();
    private List<IDoTEffect> doTEffects = new();
    private List<IDamageEffect> damageEffects = new();
    private List<IRootEffect> rootEffects = new();

    private EffectManagerData effectManagerData;

    public UnityEvent<eEffectType, eEffectStatus> EffectEvent { get; } = new();

    public EffectManager(MonoBehaviour owner, EffectManagerData effectManagerData)
    {
        this.owner = owner;
        this.effectManagerData = effectManagerData;

        owner.StartCoroutine(CheckEffectsCoroutine());
    }

    private IEnumerator CheckEffectsCoroutine()
    {
        while (true)
        {
            RemoveExpiredEffects();

            DealDoTDamage();

            yield return new WaitForSeconds(effectManagerData.checkEffectsPeriod);
        }
    }

    private void RemoveExpiredEffects()
    {
        List<IEffect> expiredEffects = new();
        foreach (IEffect effect in effects)
        {
            if (Time.time < effect.EndEffectTime)
            {
                continue;
            }
            expiredEffects.Add(effect);
        }
        foreach (IEffect effect in expiredEffects)
        {
            RemoveEffect(effect);
        }
    }

    private void CheckStunEffects()
    {
        if (stunEffects.Count < 1)
        {
            EffectEvent.Invoke(eEffectType.Stun, eEffectStatus.Cleared);
        }
    }

    private void CheckRootEffects()
    {
        if (rootEffects.Count < 1)
        {
            EffectEvent.Invoke(eEffectType.Root, eEffectStatus.Cleared);
        }
    }

    private void CheckDoTEffects()
    {
        if (rootEffects.Count < 1)
        {
            EffectEvent.Invoke(eEffectType.DoT, eEffectStatus.Cleared);
        }
    }

    private void DealDoTDamage()
    {
        foreach (IDoTEffect doTEffect in doTEffects)
        {
            if (Time.time - doTEffect.LastTickTime < doTEffect.DamagePeriod)
            {
                continue;
            }

            doTEffect.DealDamage();
            doTEffect.LastTickTime = Time.time;
        }
    }

    public virtual void AddEffect(IEffect effect)
    {
        effects.Add(effect);

        switch (effect)
        {
            case IStunEffect:
                stunEffects.Add(effect as IStunEffect);
                EffectEvent.Invoke(eEffectType.Stun, eEffectStatus.Added);
                break;

            case ISlowEffect:
                slowEffects.Add(effect as ISlowEffect);
                EffectEvent.Invoke(eEffectType.Slow, eEffectStatus.Added);
                break;

            case IDoTEffect:
                doTEffects.Add(effect as IDoTEffect);
                EffectEvent.Invoke(eEffectType.DoT, eEffectStatus.Added);
                break;

            case IDamageEffect:
                damageEffects.Add(effect as IDamageEffect);
                EffectEvent.Invoke(eEffectType.Damage, eEffectStatus.Added);
                break;

            case IRootEffect:
                rootEffects.Add(effect as IRootEffect);
                EffectEvent.Invoke(eEffectType.Root, eEffectStatus.Added);
                break;
        }
    }
    public void RemoveEffect(IEffect effect)
    {
        if (effects.Remove(effect) == false)
        {
            return;
        }

        switch (effect)
        {
            case IStunEffect:
                stunEffects.Remove(effect as IStunEffect);
                EffectEvent.Invoke(eEffectType.Stun, eEffectStatus.Removed);
                CheckStunEffects();
                break;

            case ISlowEffect:
                slowEffects.Remove(effect as ISlowEffect);
                EffectEvent.Invoke(eEffectType.Slow, eEffectStatus.Removed);
                break;

            case IDoTEffect:
                doTEffects.Remove(effect as IDoTEffect);
                EffectEvent.Invoke(eEffectType.DoT, eEffectStatus.Removed);
                //CheckDoTEffects();
                break;

            case IDamageEffect:
                damageEffects.Remove(effect as IDamageEffect);
                EffectEvent.Invoke(eEffectType.Damage, eEffectStatus.Removed);
                break;

            case IRootEffect:
                rootEffects.Remove(effect as IRootEffect);
                EffectEvent.Invoke(eEffectType.Root, eEffectStatus.Removed);
                CheckRootEffects();
                break;
        }
    }

    public void ClearEffects(eEffectType effectType)
    {
        switch (effectType)
        {
            case eEffectType.Stun:
                stunEffects.Clear();
                break;

            case eEffectType.Slow:
                slowEffects.Clear();
                break;

            case eEffectType.DoT:
                doTEffects.Clear();
                break;

            case eEffectType.Damage:
                damageEffects.Clear();
                break;

            case eEffectType.Root:
                rootEffects.Clear();
                break;
        }

        EffectEvent.Invoke(effectType, eEffectStatus.Cleared);
    }

    public void ApplyDamageEffects(Damage damage)
    {
        foreach (IDamageEffect damageEffect in damageEffects)
        {
            damageEffect.ApplyEffect(damage);
        }
    }

    public float GetMaxStunDuration()
    {
        float maxStunDuration = 0f;

        foreach (IStunEffect stunEffect in stunEffects)
        {
            float remainingStunDuration = stunEffect.EndEffectTime - Time.time;
            if (remainingStunDuration > maxStunDuration)
            {
                maxStunDuration = remainingStunDuration;
            }
        }
        return maxStunDuration;
    }

    public float GetMaxEffectDuration(eEffectType effectType)
    {
        float maxDuration = 0f;

        IList effects = null;
        switch (effectType)
        {
            case eEffectType.Stun:
                effects = stunEffects;
                break;
            case eEffectType.Slow:
                effects = slowEffects;
                break;
            case eEffectType.DoT:
                effects = doTEffects;
                break;
            case eEffectType.Damage:
                effects = damageEffects;
                break;
            case eEffectType.Root:
                effects = rootEffects;
                break;
        }

        foreach (IEffect effect in effects)
        {
            float remainingStunDuration = effect.EndEffectTime - Time.time;
            if (remainingStunDuration > maxDuration)
                maxDuration = remainingStunDuration;
        }

        return maxDuration;
    }

    public float GetCumulativeSlowEffect()
    {
        float remainingSpeed = 1f;

        foreach (ISlowEffect slowEffect in slowEffects)
        {
            remainingSpeed *= 1f - slowEffect.MovementSlowValue;
        }

        return (1f - remainingSpeed);
    }

    public float GetCumulativeDamagePerSecond()
    {
        float damagePerSecond = 0f;

        foreach (IDoTEffect doTEffect in doTEffects)
        {
            damagePerSecond += doTEffect.Damage.EffectiveDamage / doTEffect.DamagePeriod;
        }

        return damagePerSecond;
    }
}
