using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageData", menuName = "Data/Damage/New Damage Data")]
public class DamageData : ScriptableObject
{
    public eDamageType damageType = eDamageType.MeleeWeapon;
    [Min(0f)]
    public float damageValue = 1f;
}
