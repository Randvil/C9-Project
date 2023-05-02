using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpiderMinionCompoundAttack : ICompoundAttack
{
    private GameObject character;
    private IWeapon weapon;
    private IAbility jumpAbility;

    public bool IsPerforming => (jumpAbility.IsPerforming || weapon.IsAttacking);

    public SpiderMinionCompoundAttack(GameObject character, IWeapon weapon, IAbility jumpAbility)
    {
        this.character = character;
        this.weapon = weapon;
        this.jumpAbility = jumpAbility;
    }

    public bool MakeAfficientAttack(Vector2 enemyPosition)
    {
        if (jumpAbility.CanBeUsed)
        {
            jumpAbility.StartCast();
            return true;
        }

        float distanceToEnemy = Vector2.Distance(character.transform.position, enemyPosition);
        if (distanceToEnemy < weapon.AttackRange)
        {
            weapon.StartAttack();
            return true;
        }

        return false;
    }

    public void BreakAttack()
    {
        if (jumpAbility.IsPerforming)
        {
            jumpAbility.BreakCast();
        }        
        weapon.BreakAttack();
    }
}
