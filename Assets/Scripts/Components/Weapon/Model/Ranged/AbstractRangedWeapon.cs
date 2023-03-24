using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractRangedWeapon : AbstractWeapon
{
    protected Transform projectileSpawnPoint;
    protected RangedWeaponData rangedWeaponData;

    protected AbstractRangedWeapon(GameObject weaponOwner, IModifierManager weaponModifierManager, ITeam team, ITurning turning, Transform projectileSpawnPoint, RangedWeaponData rangedWeaponData) : base(weaponOwner, rangedWeaponData.weaponData, weaponModifierManager, team, turning)
    {
        this.projectileSpawnPoint = projectileSpawnPoint;
        this.rangedWeaponData = rangedWeaponData;
    }
}
