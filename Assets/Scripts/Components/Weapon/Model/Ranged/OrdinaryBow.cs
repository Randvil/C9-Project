using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryBow : AbstractRangedWeapon
{
    public OrdinaryBow(MonoBehaviour owner, GameObject weaponOwner, Transform projectileSpawnPoint, RangedWeaponData bowWeaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, weaponModifierManager, team, turning, projectileSpawnPoint, bowWeaponData) { }

    protected override void ReleaseAttack(int attackNumber)
    {
        DamageData damageData = damageDatas.Length >= attackNumber ? damageDatas[attackNumber - 1] : damageDatas[0];

        IProjectile projectile = Object.Instantiate(projectilePrefab, new(projectileSpawnPoint.position.x, projectileSpawnPoint.position.y, 0f), Quaternion.identity).GetComponent<IProjectile>();
        projectile.Release(weaponOwnerObject, damageData, turning.Direction, team, weaponModifierManager, this);
    }
}