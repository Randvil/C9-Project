using System;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    private IModifierManager modifierManager;

    public GameObject OwnerObject { get; private set; }
    public GameObject SourceObject { get; private set; }
    public eDamageType DamageType { get; private set; }
    public float BaseDamage { get; private set; }
    public float EffectiveDamage => modifierManager.ApplyModifiers(BaseDamage);

    public Damage(GameObject ownerObject, GameObject sourceObject, DamageData damageData, IModifierManager modifierManager)
    {
        OwnerObject = ownerObject;
        SourceObject = sourceObject;

        DamageType = damageData.damageType;
        BaseDamage = damageData.damageValue;

        this.modifierManager = modifierManager;
    }
}