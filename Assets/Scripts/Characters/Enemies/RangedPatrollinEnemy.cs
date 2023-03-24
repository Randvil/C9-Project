using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPatrollinEnemy : BasePatrollingEnemy
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private RangedWeaponData rangeWeaponData;

    public override IWeapon Weapon { get; protected set; }

    protected override void Start()
    {
        base.Start();

        Weapon = new OrdinaryBow(gameObject, projectileSpawnPoint, rangeWeaponData, WeaponModifierManager, this, Turning);
    }
}
