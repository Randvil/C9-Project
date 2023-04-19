using UnityEngine;

[CreateAssetMenu(fileName = "New Base Ability Data", menuName = "Component Data/Model/New Base Ability Data", order = 300)]
public class BaseAbilityData : ScriptableObject
{
    [Header("Base Ability Data")]
    public float cooldown;
    public float preCastDelay;
    public float postCastDelay;
    public float cost;
}
