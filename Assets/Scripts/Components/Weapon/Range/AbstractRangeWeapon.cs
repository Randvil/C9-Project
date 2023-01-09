using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractRangeWeapon : AbstractWeapon
{
    [SerializeField]
    protected GameObject projectilePrefab;

    [SerializeField]
    protected float projectileSpeed;

    [SerializeField]
    protected float projectileRotation;

    protected eTeam weaponOwnerTeam;

    private void Start()
    {
        weaponOwnerTeam = GetComponent<ITeam>().Team;
    }

    protected override void PrepareAttack()
    {
        base.PrepareAttack();
    }

    protected override void FinishAttack()
    {
        base.FinishAttack();
    }
}
