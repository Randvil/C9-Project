using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryBow : AbstractRangeWeapon
{
    protected Turning turning;

    protected override void Start()
    {
        base.Start();

        turning = GetComponent<Turning>();
    }

    protected override void ReleaseAttack()
    {
        IProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0f, (float)turning.Direction, 0f))).GetComponent<IProjectile>();
        projectile.Initialize(turning.Direction, projectileRotation, projectileSpeed, weaponOwnerTeam, damage);
    }

}