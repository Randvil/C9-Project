using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseAbilityData", menuName = "Data/Abilities/Base Ability/New Base Ability Data")]
public class BaseAbilityData : ScriptableObject
{
    [Header("Base Ability Data")]
    public eAbilityType abilityType;
    [Min(0f)]
    public float cooldown;
    [Min(0f)]
    public float preCastDelay;
    [Min(0f)]
    public float postCastDelay;
    [Min(0f), Tooltip("One time cost for instant abilities, cost per second for periodic abilities or cost for each instance for abilities that create multiple instances [energy]")]
    public float cost;
}
