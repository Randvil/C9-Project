using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageReflection : IDamageEffect
{
    public virtual void ApplyEffect(Damage incomingDamage)
    {
        if (incomingDamage.sourceWeapon is AbstractMeleeWeapon)
            incomingDamage.sourceObject.GetComponent<IDamageHandler>().TakeDamage(incomingDamage);
    }
}
