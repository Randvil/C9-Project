using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryBow : AbstractRangedWeapon
{
    public OrdinaryBow(GameObject weaponOwner, Transform projectileSpawnPoint, RangedWeaponData rangedWeaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(weaponOwner, weaponModifierManager, team, turning, projectileSpawnPoint, rangedWeaponData) { }

    protected override void ReleaseAttack()
    {
        IProjectile projectile = Object.Instantiate(rangedWeaponData.projectileData.prefab, new(projectileSpawnPoint.position.x, projectileSpawnPoint.position.y, 0f), Quaternion.identity).GetComponent<IProjectile>();
        projectile.Initialize(weaponOwner, rangedWeaponData.weaponData.damageData, rangedWeaponData.projectileData, turning.Direction, team, weaponModifierManager);
    }
}