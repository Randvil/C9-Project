using System;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    private DamageData damageData;

    private IModifierManager modifierManager;

    public GameObject OwnerObject { get; private set; }
    public GameObject SourceObject { get; private set; }
    public eDamageType DamageType => damageData.damageType;
    public float BaseDamage => damageData.damageValue;
    public float EffectiveDamage => modifierManager.ApplyModifiers(BaseDamage);

    public Damage(GameObject ownerObject, GameObject sourceObject, DamageData damageData, IModifierManager modifierManager)
    {
        OwnerObject = ownerObject;
        SourceObject = sourceObject;

        this.damageData = damageData;

        this.modifierManager = modifierManager;
    }
}