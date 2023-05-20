using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeDamageReflection : BaseEffect, IDamageEffect
{
    private IDamageDealer damageDealer;

    public UnityEvent DamageEffectEvent { get; } = new();

    public MeleeDamageReflection(EffectData effectData, IDamageDealer damageDealer) : base(effectData)
    {
        this.damageDealer = damageDealer;
    }

    public virtual void ApplyEffect(Damage incomingDamage)
    {
        if (incomingDamage.DamageType == eDamageType.MeleeWeapon)
        {
            incomingDamage.SourceObject.GetComponent<IDamageable>().DamageHandler.TakeDamage(incomingDamage, damageDealer);
            DamageEffectEvent.Invoke();
        }
    }
}
