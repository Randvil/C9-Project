using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMinionCompoundAttack : ICompoundAttack
{
    private GameObject character;

    private float minJumpDistance;
    private float maxJumpDistance;
    private float baseAttackDistance;

    private IWeapon weapon;
    private IAbility jumpAbility;

    public bool IsPerforming => (jumpAbility.IsPerforming || weapon.IsAttacking);

    public SpiderMinionCompoundAttack(GameObject character, SpiderMinionCompoundAttackData spiderMinionCompoundAttackData, IWeapon weapon, IAbility jumpAbility)
    {
        this.character = character;

        minJumpDistance = spiderMinionCompoundAttackData.minJumpDistance;
        maxJumpDistance = spiderMinionCompoundAttackData.maxJumpDistance;
        baseAttackDistance = spiderMinionCompoundAttackData.baseAttackDistance;

        this.weapon = weapon;
        this.jumpAbility = jumpAbility;
    }

    public bool MakeAfficientAttack(Vector2 enemyPosition)
    {
        float distanceToEnemy = Vector2.Distance(character.transform.position, enemyPosition);

        if (jumpAbility.CanBeUsed && distanceToEnemy > minJumpDistance && distanceToEnemy < maxJumpDistance)
        {
            jumpAbility.StartCast();
            return true;
        }

        if (distanceToEnemy < baseAttackDistance)
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
        if (weapon.IsAttacking)
        {
            weapon.BreakAttack();
        }
    }
}
