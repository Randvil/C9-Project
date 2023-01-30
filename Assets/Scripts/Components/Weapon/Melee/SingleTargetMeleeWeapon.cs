using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetMeleeWeapon : AbstractMeleeWeapon
{
    protected Turning turning;

    protected virtual void Start()
    {
        turning = GetComponent<Turning>();
    }

    protected override void ReleaseAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayerMask);

        if (enemies.Length == 0)
            return;

        IDamageHandler nearestEnemy = null;
        float distanceToNearestEnemy = float.MaxValue;

        foreach (Collider2D enemy in enemies)
        {
            IDamageHandler damageableEnemy = enemy.GetComponent<IDamageHandler>();

            if (damageableEnemy == null)
                continue;

            if ((turning.Direction == eDirection.Right && enemy.transform.position.x >= transform.position.x) || (turning.Direction == eDirection.Left && enemy.transform.position.x <= transform.position.x))
            {
                if (Mathf.Abs(enemy.transform.position.x - transform.position.x) < distanceToNearestEnemy)
                    nearestEnemy = damageableEnemy;
            }
        }

        if (nearestEnemy != null)
            nearestEnemy.TakeDamage(damage);
    }
}
