using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeDamageReflection : IDamageEffect
{
    public float EndEffectTime { get; private set; }

    public UnityEvent DamageEffectEvent { get; } = new();

    public MeleeDamageReflection(float endEffectTime)
    {
        EndEffectTime = endEffectTime;
    }

    public virtual void ApplyEffect(Damage incomingDamage)
    {
        if (incomingDamage.DamageType == eDamageType.MeleeWeapon)
        {
            incomingDamage.SourceObject.GetComponent<IDamageable>().DamageHandler.TakeDamage(incomingDamage);
            DamageEffectEvent.Invoke();
        }
    }
}
