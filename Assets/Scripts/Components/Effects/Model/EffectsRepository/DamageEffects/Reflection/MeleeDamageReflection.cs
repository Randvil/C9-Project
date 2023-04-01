using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeDamageReflection : IDamageEffect
{
    private IDamageDealer damageDealer;

    public float EndEffectTime { get; private set; }

    public UnityEvent DamageEffectEvent { get; } = new();

    public MeleeDamageReflection(float endEffectTime, IDamageDealer damageDealer)
    {
        EndEffectTime = endEffectTime;
        this.damageDealer = damageDealer;
    }

    public virtual void ApplyEffect(Damage incomingDamage)
    {
        if (incomingDamage.DamageType == eDamageType.MeleeWeapon)
        {
            incomingDamage.SourceObject.GetComponent<IDamageable>().DamageHandler.TakeDamage(incomingDamage, damageDealer.DealDamageEvent);
            DamageEffectEvent.Invoke();
        }
    }
}
