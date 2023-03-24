using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetMeleeWeapon : AbstractMeleeWeapon
{
    public SingleTargetMeleeWeapon(GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(weaponOwner, weaponData, weaponModifierManager, team, turning) { }

    protected override void ReleaseAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponOwner.transform.position, weaponData.attackRadius);

        if (enemies.Length == 0)
        {
            return;
        }            

        IDamageable nearestDamageableEnemy = null;
        float distanceToNearestEnemy = float.MaxValue;

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out ITeam enemyTeam) == false || enemyTeam.Team == team.Team)
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == false)
            {
                continue;
            }

            if (((turning.Direction == eDirection.Right && enemy.transform.position.x >= weaponOwner.transform.position.x)
                || (turning.Direction == eDirection.Left && enemy.transform.position.x <= weaponOwner.transform.position.x))
                && Mathf.Abs(enemy.transform.position.x - weaponOwner.transform.position.x) < distanceToNearestEnemy)
            {
                nearestDamageableEnemy = damageableEnemy;
            }
        }

        if (nearestDamageableEnemy != null)
        {
            nearestDamageableEnemy.DamageHandler.TakeDamage(damage);
        }            
    }
}