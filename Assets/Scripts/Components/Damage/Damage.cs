using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Damage
{
    public float baseDamage;
    public eDamageType damageType;
    [HideInInspector] public GameObject sourceObject;
    public IWeapon sourceWeapon;
    public List<IDamageModificator> modificators;
}