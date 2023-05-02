using UnityEngine;

[CreateAssetMenu(fileName = "NewKanaboData", menuName = "Data/Abilities/New Kanabo Data")]
public class KanaboData : DamageAbilityData
{
    [Header("Kanabo Data")]
    [Min(0f)]
    public float attackRange = 1f;
    public StunEffectData stunEffectData;
}
