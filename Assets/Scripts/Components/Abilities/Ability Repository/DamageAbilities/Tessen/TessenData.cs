using UnityEngine;

[CreateAssetMenu(fileName = "NewTessenData", menuName = "Data/Abilities/New Tessen Data")]
public class TessenData : DamageAbilityData
{
    [Header("Tessen Data")]
    [Min(0f)]
    public float attackRange = 5f;
    [Min(0f)]
    public float castTime = 3f;
    [Min(0.01f), Tooltip("How often unaffected characters will be affected by the ability, and those already affected will receive damage [seconds]")]
    public float impactPeriod = 0.25f;
    [Min(0f), Tooltip("Vertical speed of hovering character [units per second]")]
    public float ascensionalPower = 2f;
    public StunEffectData stunEffectData;
}
