using UnityEngine;

[CreateAssetMenu(fileName = "New Tessen Data", menuName = "Component Data/Model/New Tessen Data", order = 313)]
public class TessenData : DamageAbilityData
{
    [Header("Tessen Data")]
    public float attackRange = 5f;
    public float castTime = 3f;
    public float impactPeriod = 0.25f;
    public float ascensionalPower = 2f;
}
