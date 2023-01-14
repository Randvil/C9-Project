using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMeleeWeapon : AbstractMeleeWeapon
{
    protected override void ReleaseAttack(eDirection direction)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayerMask);

        foreach (Collider2D enemy in enemies)
        {
            IDamageHandler damageableEnemy = enemy.GetComponent<IDamageHandler>();
            if (damageableEnemy == null)
                continue;

            if ((direction == eDirection.Right && enemy.transform.position.x >= transform.position.x) || (direction == eDirection.Left && enemy.transform.position.x <= transform.position.x))
                damageableEnemy.TakeDamage(damage, this);
        }
    }
}