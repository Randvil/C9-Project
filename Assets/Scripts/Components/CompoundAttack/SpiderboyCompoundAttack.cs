using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderboyCompoundAttack : ICompoundAttack
{
    private GameObject spiderboy;
    private IWeapon weapon;
    private IAbility summonSpiderAbility;

    public SpiderboyCompoundAttack(GameObject spiderboy, IWeapon weapon, IAbility ability)
    {
        this.spiderboy = spiderboy;
        this.weapon = weapon;
        this.summonSpiderAbility = ability;
    }

    public bool MakeAfficientAttack(Vector2 enemyPosition)
    {
        if (summonSpiderAbility.IsPerforming)
        {
            return true;
        }

        if (summonSpiderAbility.CanBeUsed)
        {
            summonSpiderAbility.StartCast();
            return true;
        }

        if (Vector2.Distance(spiderboy.transform.position, enemyPosition) < weapon.AttackRange)
        {
            weapon.StartAttack();
            return true;
        }

        return false;
    }

    public void BreakAttack()
    {
        summonSpiderAbility.BreakCast();
        weapon.BreakAttack();
    }
}
