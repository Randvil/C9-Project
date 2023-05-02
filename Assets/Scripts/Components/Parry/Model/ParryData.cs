using UnityEngine;

[CreateAssetMenu(fileName = "NewParryData", menuName = "Data/Components/Parry/New Parry Data")]
public class ParryData : ScriptableObject
{
    [Min(0f), Tooltip("Maximum stance duration, [seconds]")]
    public float duration = 0.5f;

    [Min(0f), Tooltip("How often the player can stance [seconds]")]
    public float cooldown = 2f;

    [Range(0f, 1f), Tooltip("Absorption value [parts of the incoming damage]")]
    public float damageAbsorption = 1f;

    [Tooltip("Should melee damage be reflected?")]
    public bool reflectMeleeDamage = false;

    [Tooltip("Should projectiles be reflected?")]
    public bool reflectProjectiles = true;

    [Min(0f), Tooltip("How much damage should be reflected? [parts of the incoming damage]")]
    public float reflectionDamageMultiplier = 1f;

    [Tooltip("Should the damage of the following attacks be increased after successful melee parry?")]
    public bool meleeAmplifyDamage = true;

    [Tooltip("Should the damage of the following attacks be increased after successful range parry?")]
    public bool rangeAmplifyDamage = false;

    [Min(0f), Tooltip("How much extra damage your Weapon should deal with the following attacks? [parts of Weapon damage]")]
    public float extraAttackDamage = 1f;

    [Min(1), Tooltip("How many attacks should be amplified?")]
    public int amplifiedAttackNumber = 1;

    [Min(0f), Tooltip("How long the attack buff will last? [seconds]")]
    public float amplifyDuration = 3f;
}
