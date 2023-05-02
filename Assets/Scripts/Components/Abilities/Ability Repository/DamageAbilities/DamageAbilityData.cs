using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageAbilityData", menuName = "Data/Abilities/New Damage Ability Data")]
public class DamageAbilityData : BaseAbilityData
{
    [Header("Damage Ability Data")]
    public DamageData damageData;
}
