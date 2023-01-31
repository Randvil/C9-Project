using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteDamageModificator : IDamageModificator
{
    private float modificator;

    private eDamageModificator order = eDamageModificator.Absolute;
    public eDamageModificator Order { get => order; }

    public AbsoluteDamageModificator(float modificator)
    {
        this.modificator = modificator;
    }

    public Damage ApplyModificator(Damage uneffectedDamage)
    {
        Damage effectedDamage = uneffectedDamage;
        
        effectedDamage.baseDamage = Mathf.Clamp(uneffectedDamage.baseDamage + modificator, 0f, float.MaxValue);

        return effectedDamage;
    }

    public int CompareTo(IDamageModificator compareEffect)
    {
        return Order.CompareTo(compareEffect.Order);
    }
}
