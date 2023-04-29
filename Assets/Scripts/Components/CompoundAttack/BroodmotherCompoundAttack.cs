using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherCompoundAttack : ICompoundAttack
{
    private GameObject broodmother;

    private IWeapon weapon;
    private IDamageAbility webAbility;
    private IDamageAbility stunAbility;

    public int Phase { get; set; } = 1;

    public BroodmotherCompoundAttack(GameObject broodmother, IWeapon weapon, IDamageAbility webAbility, IDamageAbility stunAbility)
    {
        this.broodmother = broodmother;

        this.weapon = weapon;
        this.webAbility = webAbility;
        this.stunAbility = stunAbility;
    }

    public bool MakeAfficientAttack(Vector2 enemyPosition)
    {
        float distanceToEnemy = Vector2.Distance(broodmother.transform.position, enemyPosition);

        if (Phase == 3)
        {
            if (webAbility.IsPerforming == true || stunAbility.IsPerforming == true)
            {
                return true;
            }

            if (webAbility.CanBeUsed && webAbility.AttackRange > distanceToEnemy)
            {
                webAbility.StartCast();
                return true;
            }

            if (stunAbility.CanBeUsed && stunAbility.AttackRange > distanceToEnemy)
            {
                stunAbility.StartCast();
                return true;
            }
        }

        if (Phase == 2)
        {
            if (stunAbility.IsPerforming == true)
            {
                return true;
            }

            if (stunAbility.CanBeUsed && stunAbility.AttackRange > distanceToEnemy)
            {
                stunAbility.StartCast();
                return true;
            }
        }

        if (weapon.AttackRange > distanceToEnemy)
        {
            weapon.StartAttack();
            return true;
        }

        return false;
    }

    public void BreakAttack()
    {
        webAbility.BreakCast();
        stunAbility.BreakCast();
        weapon.BreakAttack();
    }
}
