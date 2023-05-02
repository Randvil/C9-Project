using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustWeaponCompoundAttack : ICompoundAttack
{
    private GameObject character;
    private IWeapon weapon;

    public bool IsPerforming => weapon.IsAttacking;

    public JustWeaponCompoundAttack(GameObject character, IWeapon weapon)
    {
        this.character = character;
        this.weapon = weapon;
    }

    public bool MakeAfficientAttack(Vector2 enemyPosition)
    {
        if (Vector2.Distance(character.transform.position, enemyPosition) < weapon.AttackRange)
        {
            weapon.StartAttack();
            return true;
        }

        return false;
    }

    public void BreakAttack()
    {
        weapon.BreakAttack();
    }
}
