using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPatrollinEnemy : BasePatrollingEnemy
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private RangedWeaponData rangeWeaponData;

    protected override void CreateWeaponWithView()
    {
        Weapon = new OrdinaryBow(gameObject, projectileSpawnPoint, rangeWeaponData, WeaponModifierManager, this, Turning);
        WeaponView = new NoArmsWeaponView(Weapon, Animator, attackAudioSource);
    }
}
