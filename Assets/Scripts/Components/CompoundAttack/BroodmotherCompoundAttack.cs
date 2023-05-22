using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherCompoundAttack : ICompoundAttack
{
    private GameObject broodmother;

    private IWeapon weapon;
    private IDamageAbility webAbility;
    private IDamageAbility stunAbility;
    private IAbility jumpAbility;
    private IAbility swarmSpawningAbility;

    public int CurrentPhase { get; private set; } = 1;

    public bool IsPerforming => (jumpAbility.IsPerforming || webAbility.IsPerforming || stunAbility.IsPerforming || weapon.IsAttacking);

    public BroodmotherCompoundAttack(GameObject broodmother, IWeapon weapon, IDamageAbility webAbility, IDamageAbility stunAbility, IAbility jumpAbility, IAbility swarmSpawningAbility)
    {
        this.broodmother = broodmother;

        this.weapon = weapon;
        this.webAbility = webAbility;
        this.stunAbility = stunAbility;
        this.jumpAbility = jumpAbility;
        this.swarmSpawningAbility = swarmSpawningAbility;
    }

    public bool MakeAfficientAttack(Vector2 enemyPosition)
    {
        float distanceToEnemy = Vector2.Distance(broodmother.transform.position, enemyPosition);

        if (CurrentPhase == 3)
        {
            if (jumpAbility.IsPerforming == true || webAbility.IsPerforming == true)
            {
                return true;
            }

            if (jumpAbility.CanBeUsed)
            {
                jumpAbility.StartCast();
                return true;
            }

            if (webAbility.CanBeUsed && webAbility.AttackRange > distanceToEnemy)
            {
                webAbility.StartCast();
                return true;
            }
        }

        if (CurrentPhase == 2)
        {
            if (webAbility.IsPerforming == true)
            {
                return true;
            }

            if (webAbility.CanBeUsed && webAbility.AttackRange > distanceToEnemy)
            {
                webAbility.StartCast();
                return true;
            }
        }

        if (stunAbility.IsPerforming == true)
        {
            return true;
        }

        if (stunAbility.CanBeUsed && stunAbility.AttackRange > distanceToEnemy)
        {
            stunAbility.StartCast();
            return true;
        }

        if (weapon.IsAttacking || weapon.AttackRange > distanceToEnemy)
        {
            weapon.StartAttack();
            return true;
        }

        return false;
    }

    public void BreakAttack()
    {
        jumpAbility.BreakCast();
        webAbility.BreakCast();
        stunAbility.BreakCast();
        weapon.BreakAttack();
    }

    public void ChangePhase(int newPhase)
    {
        Debug.Log("Change Phase");

        if (newPhase > CurrentPhase)
        {
            swarmSpawningAbility.StartCast();
        }

        CurrentPhase = newPhase;
    }
}
