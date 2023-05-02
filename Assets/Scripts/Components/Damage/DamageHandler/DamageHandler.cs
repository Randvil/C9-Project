using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : IDamageHandler
{
    public UnityEvent<DamageInfo> TakeDamageEvent { get; private set; } = new();

    protected IHealthManager healthManager;
    protected IModifierManager defenceModifierManager;
    protected IEffectManager effectManager;
    protected IDeathManager deathManager;

    public DamageHandler(IHealthManager healthManager, IModifierManager defenceModifierManager, IEffectManager effectManager, IDeathManager deathManager)
    {
        this.healthManager = healthManager;
        this.defenceModifierManager = defenceModifierManager;
        this.effectManager = effectManager;
        this.deathManager = deathManager;
    }

    public void TakeDamage(Damage incomingDamage, IDamageDealer damageDealer)
    {
        if (deathManager.IsAlive == false)
        {
            return;
        }

        effectManager.ApplyDamageEffects(incomingDamage);

        float effectiveDamage = defenceModifierManager.ApplyModifiers(incomingDamage.EffectiveDamage);
        ChangeHealth(effectiveDamage);

        InvokeEvents(incomingDamage, effectiveDamage, damageDealer.DealDamageEventCallback);
    }

    protected virtual void ChangeHealth(float effectiveDamage)
    {
        healthManager.ChangeCurrentHealth(-effectiveDamage);
    }

    protected void InvokeEvents(Damage incomingDamage, float effectiveDamage, UnityEvent<DamageInfo> dealDamageEvent)
    {
        DamageInfo damageInfo = new DamageInfo
        {
            damageDealtTime = Time.time,
            damageOwnerObject = incomingDamage.OwnerObject,
            damageSourceObject = incomingDamage.SourceObject,
            damageType = incomingDamage.DamageType,
            incomingDamageValue = incomingDamage.EffectiveDamage,
            effectiveDamageValue = effectiveDamage,
        };

        dealDamageEvent.Invoke(damageInfo);
        TakeDamageEvent.Invoke(damageInfo);
    }
}
