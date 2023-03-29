using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Ability Data", menuName = "Component Data/Model/New Damage Ability Data", order = 310)]
public class DamageAbilityData : BaseAbilityData
{
    [Header("Damage Ability Data")]
    public DamageData damageData;
}
