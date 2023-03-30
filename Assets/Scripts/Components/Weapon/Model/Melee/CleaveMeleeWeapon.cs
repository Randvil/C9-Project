using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveMeleeWeapon : AbstractMeleeWeapon
{
    public CleaveMeleeWeapon(GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(weaponOwner, weaponData, weaponModifierManager, team, turning) { }

    protected override void ReleaseAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponOwner.transform.position, weaponData.attackRadius);

        if (enemies.Length == 0)
        {
            return;
        }            

        foreach (Collider2D enemy in enemies)
        {            
            if (enemy.TryGetComponent(out ITeam enemyTeam) == false || enemyTeam.Team == team.Team)
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                if ((turning.Direction == eDirection.Right && enemy.transform.position.x >= weaponOwner.transform.position.x)
                    || (turning.Direction == eDirection.Left && enemy.transform.position.x <= weaponOwner.transform.position.x))
                {
                    damageableEnemy.DamageHandler.TakeDamage(damage);
                }
            }
        }
    }
}