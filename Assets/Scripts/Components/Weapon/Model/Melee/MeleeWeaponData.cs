using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponData", menuName = "Data/Weapon/Melee/New Melee Weapon Data")]
public class MeleeWeaponData : WeaponData
{
    [Header("Melee Weapon Data")]
    [Min(0f), Tooltip("The weapon will deal damage even if the enemy is behind the attacker at this distance [units]")]
    public float backAttackDistance = 0.5f;
}
