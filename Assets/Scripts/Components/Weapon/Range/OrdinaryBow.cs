using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryBow : AbstractRangeWeapon
{
    protected override void ReleaseAttack(eDirection direction)
    {
        IProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0f, (float)direction, 0f))).GetComponent<IProjectile>();
        projectile.Initialize(direction, projectileRotation, projectileSpeed, weaponOwnerTeam, damage);
    }

}
