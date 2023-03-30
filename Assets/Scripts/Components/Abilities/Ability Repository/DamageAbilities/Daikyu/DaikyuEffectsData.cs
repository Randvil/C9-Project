using UnityEngine;

[CreateAssetMenu(fileName = "New Daikyu Effects Data", menuName = "Component Data/Model/New Daikyu Effects Data", order = 314)]
public class DaikyuEffectsData : ScriptableObject
{
    [Header("DoT")]
    public DamageData doTDamageData;
    public float doTDuration;
    public float damagePeriod;

    [Header("Slow")]
    public float movementSlow;
    public float slowDuration;
}
