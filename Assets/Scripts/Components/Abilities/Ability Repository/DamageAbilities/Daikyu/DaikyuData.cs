using UnityEngine;

[CreateAssetMenu(fileName = "New Daikyu Data", menuName = "Component Data/Model/New Daikyu Data", order = 312)]
public class DaikyuData : DamageAbilityData
{
    [Header("Daikyu Data")]
    public ProjectileData projectileData;
    public float fullChargeTime = 1f;
    public float fullChargeDamageMultiplier = 2f;
}
