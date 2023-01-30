using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeDamageModificator : IDamageModificator
{
    private float modificator;

    private eDamageModificator order = eDamageModificator.Relative;
    public eDamageModificator Order { get => order; }

    public RelativeDamageModificator(float modificator)
    {
        this.modificator = modificator;
    }

    public  Damage ApplyModificator(Damage uneffectedDamage)
    {
        Damage effectedDamage = uneffectedDamage;

        effectedDamage.baseDamage = Mathf.Clamp(1f + modificator, 0f, float.MaxValue) * uneffectedDamage.baseDamage;

        return effectedDamage;
    }

    public int CompareTo(IDamageModificator compareEffect)
    {
        return Order.CompareTo(compareEffect.Order);
    }
}
