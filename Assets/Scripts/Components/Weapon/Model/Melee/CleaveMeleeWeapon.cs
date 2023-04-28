using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveMeleeWeapon : AbstractMeleeWeapon
{
    public CleaveMeleeWeapon(MonoBehaviour owner, GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, weaponData, weaponModifierManager, team, turning) { }

    protected override void ReleaseAttack(int attackNumber)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponOwnerObject.transform.position, attackRange);

        if (enemies.Length == 0)
        {
            return;
        }            

        foreach (Collider2D enemy in enemies)
        {            
            if (enemy.TryGetComponent(out ITeamMember enemyTeam) == false || enemyTeam.CharacterTeam.Team == team.Team)
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                if ((turning.Direction == eDirection.Right && enemy.transform.position.x >= weaponOwnerObject.transform.position.x)
                    || (turning.Direction == eDirection.Left && enemy.transform.position.x <= weaponOwnerObject.transform.position.x))
                {
                    Damage damage = damages.Length >= attackNumber ? damages[attackNumber - 1] : damages[0];
                    damageableEnemy.DamageHandler.TakeDamage(damage, DealDamageEvent);
                }
            }
        }
    }
}