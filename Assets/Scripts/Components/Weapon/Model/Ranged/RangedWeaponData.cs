using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon Data", menuName = "Component Data/Model/New Ranged Weapon Data", order = 220)]
public class RangedWeaponData : ScriptableObject
{
    public WeaponData weaponData;
    public ProjectileData projectileData;
}
