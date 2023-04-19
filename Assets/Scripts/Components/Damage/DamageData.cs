using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Data", menuName = "Component Data/Model/New Damage Data", order = 100)]
public class DamageData : ScriptableObject
{
    public eDamageType damageType = eDamageType.MeleeWeapon;
    public float damageValue = 1f;
}
