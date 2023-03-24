using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePatrollingEnemy : BasePatrollingEnemy
{
    [SerializeField] private WeaponData weaponData;

    public override IWeapon Weapon { get; protected set; }

    protected override void Start()
    {
        base.Start();

        Weapon = new SingleTargetMeleeWeapon(gameObject, weaponData, WeaponModifierManager, this, Turning);
    }
}
