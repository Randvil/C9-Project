using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : IDamageHandler
{
    public UnityEvent<DamageInfo> TakeDamageEvent { get; private set; } = new();

    private IHealthManager healthManager;
    private IModifierManager defenceModifierManager;
    private IEffectManager effectManager;

    public DamageHandler(IHealthManager healthManager, IModifierManager defenceModifierManager, IEffectManager effectManager)
    {
        this.healthManager = healthManager;
        this.defenceModifierManager = defenceModifierManager;
        this.effectManager = effectManager;
    }

    public void TakeDamage(Damage incomingDamage, UnityEvent<DamageInfo> dealDamageEvent)
    {
        effectManager.ApplyDamageEffects(incomingDamage);

        float effectiveDamage = defenceModifierManager.ApplyModifiers(incomingDamage.EffectiveDamage);

        healthManager.ChangeCurrentHealth(-effectiveDamage);

        InvokeEvents(incomingDamage, effectiveDamage, dealDamageEvent);
    }

    private void InvokeEvents(Damage incomingDamage, float effectiveDamage, UnityEvent<DamageInfo> dealDamageEvent)
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
