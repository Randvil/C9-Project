using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveMeleeWeapon : AbstractMeleeWeapon
{
    protected float backAttackDistance;

    public CleaveMeleeWeapon(MonoBehaviour owner, GameObject weaponOwner, MeleeWeaponData meleeWeaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, meleeWeaponData, weaponModifierManager, team, turning)
    {
        backAttackDistance = meleeWeaponData.backAttackDistance;
    }

    protected override void ReleaseAttack(int attackNumber)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponOwnerObject.transform.position, attackRange);

        if (enemies.Length == 0)
        {
            return;
        }            

        foreach (Collider2D enemy in enemies)
        {            
            if (team.IsSame(enemy))
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                if ((turning.Direction == eDirection.Right && enemy.transform.position.x >= weaponOwnerObject.transform.position.x - backAttackDistance)
                    || (turning.Direction == eDirection.Left && enemy.transform.position.x <= weaponOwnerObject.transform.position.x + backAttackDistance))
                {
                    Damage damage = damages.Length >= attackNumber ? damages[attackNumber - 1] : damages[0];
                    damageableEnemy.DamageHandler.TakeDamage(damage, this);
                }
            }
        }
    }
}