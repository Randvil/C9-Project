using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : AbstractRangedWeapon
{
    public BombThrower(MonoBehaviour owner, GameObject weaponOwner, Transform projectileSpawnPoint, RangedWeaponData rangedWeaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, weaponModifierManager, team, turning, projectileSpawnPoint, rangedWeaponData)
    {
    }

    protected override void ReleaseAttack(int attackNumber)
    {
        DamageData damageData = damageDatas.Length >= attackNumber ? damageDatas[attackNumber - 1] : damageDatas[0];

        IProjectile projectile = Object.Instantiate(prefab, new(projectileSpawnPoint.position.x, projectileSpawnPoint.position.y, 0f), Quaternion.identity).GetComponent<IProjectile>();
        projectile.Initialize(weaponOwnerObject, damageData, projectileData, turning.Direction, team, weaponModifierManager, this);
        
        if (projectile is Bomb bomb)
        {
            bomb.Initialize(projectileData as BombData);
        }
    }
}
