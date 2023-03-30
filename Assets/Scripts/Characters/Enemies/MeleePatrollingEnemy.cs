using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePatrollingEnemy : BasePatrollingEnemy
{
    [SerializeField] private WeaponData weaponData;

    protected override void CreateWeaponWithView()
    {
        Weapon = new SingleTargetMeleeWeapon(gameObject, weaponData, WeaponModifierManager, this, Turning);
        WeaponView = new NoArmsWeaponView(Weapon, Animator, attackAudioSource);
    }
}
