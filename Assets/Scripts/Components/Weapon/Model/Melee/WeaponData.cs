using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Component Data/Model/New Weapon Data", order = 180)]
public class WeaponData : ScriptableObject
{
    public DamageData[] damageDatas;
    public float attackRange = 1f;
    public float[] preAttackDelays =
    {
        0.5f
    };
    public float postAttackDelay = 0.5f;
    public float attackSpeed = 1f;
}
