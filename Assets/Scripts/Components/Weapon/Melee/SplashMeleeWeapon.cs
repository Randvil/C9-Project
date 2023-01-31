using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMeleeWeapon : AbstractMeleeWeapon
{
    protected Turning turning;

    protected virtual void Start()
    {
        turning = GetComponent<Turning>();
    }

    protected override void ReleaseAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayerMask);

        foreach (Collider2D enemy in enemies)
        {
            IDamageHandler damageableEnemy = enemy.GetComponent<IDamageHandler>();
            if (damageableEnemy == null)
                continue;

            if ((turning.Direction == eDirection.Right && enemy.transform.position.x >= transform.position.x) || (turning.Direction == eDirection.Left && enemy.transform.position.x <= transform.position.x))
                damageableEnemy.TakeDamage(damage);
        }
    }
}