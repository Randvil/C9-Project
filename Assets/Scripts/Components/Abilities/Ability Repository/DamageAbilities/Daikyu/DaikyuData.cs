using UnityEngine;

[CreateAssetMenu(fileName = "NewDaikyuData", menuName = "Data/Abilities/New Daikyu Data")]
public class DaikyuData : DamageAbilityData
{
    [Header("Daikyu Data")]
    public GameObject projectilePrefab;
    [Min(0f), Tooltip("Charge time after which the ability deals maximum damage [seconds]")]
    public float fullChargeTime = 1f;
    [Min(0f), Tooltip("Maximum damage multiplier when fully charged")]
    public float fullChargeDamageMultiplier = 2f;
}
