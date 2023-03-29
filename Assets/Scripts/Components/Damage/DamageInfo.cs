using UnityEngine;

public struct DamageInfo
{
    public float damageDealtTime;
    public GameObject damageOwnerObject;
    public GameObject damageSourceObject;
    public eDamageType damageType;
    public float incomingDamageValue;
    public float effectiveDamageValue;
}
