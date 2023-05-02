using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetMeleeWeapon : AbstractMeleeWeapon
{
    public SingleTargetMeleeWeapon(MonoBehaviour owner, GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, weaponData, weaponModifierManager, team, turning) { }

    protected override void ReleaseAttack(int attackNumber)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponOwnerObject.transform.position, attackRange);

        if (enemies.Length == 0)
        {
            return;
        }            

        IDamageable nearestDamageableEnemy = null;
        float distanceToNearestEnemy = float.MaxValue;

        foreach (Collider2D enemy in enemies)
        {
            if (team.IsSame(enemy))
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == false)
            {
                continue;
            }

            if (((turning.Direction == eDirection.Right && enemy.transform.position.x >= weaponOwnerObject.transform.position.x)
                || (turning.Direction == eDirection.Left && enemy.transform.position.x <= weaponOwnerObject.transform.position.x))
                && Mathf.Abs(enemy.transform.position.x - weaponOwnerObject.transform.position.x) < distanceToNearestEnemy)
            {
                nearestDamageableEnemy = damageableEnemy;
            }
        }

        if (nearestDamageableEnemy != null)
        {
            Damage damage = damages.Length >= attackNumber ? damages[attackNumber - 1] : damages[0];
            nearestDamageableEnemy.DamageHandler.TakeDamage(damage, this);
        }            
    }
}