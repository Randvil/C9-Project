using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Data/Weapon/New Weapon Data")]
public class WeaponData : ScriptableObject
{
    public DamageData[] damageDatas;
    [Min(0f)]
    public float attackRange = 1f;
    [Tooltip("The number of delays determines the length of the series of attacks. Needs to match the animation")]
    public float[] preAttackDelays =
    {
        0.5f
    };
    [Min(0f)]
    public float postAttackDelay = 0.5f;
    [Min(0f)]
    public float attackSpeed = 1f;
}
