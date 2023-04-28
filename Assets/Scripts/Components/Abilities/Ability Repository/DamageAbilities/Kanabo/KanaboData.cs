using UnityEngine;

[CreateAssetMenu(fileName = "New Kanabo Data", menuName = "Component Data/Model/New Kanabo Data", order = 311)]
public class KanaboData : DamageAbilityData
{
    [Header("Kanabo Data")]
    public float attackRange = 1f;
    public float stunDuration = 1f;
}
