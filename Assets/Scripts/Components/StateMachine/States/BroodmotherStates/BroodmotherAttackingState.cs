using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherAttackingState : IState
{
    private BroodmotherStrategy broodmother;

    public BroodmotherAttackingState(BroodmotherStrategy broodmother)
    {
        this.broodmother = broodmother;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        broodmother.CompoundAttack.BreakAttack();
    }

    public void LogicUpdate()
    {
        if (broodmother.Enemy == null)
        {
            broodmother.StateMachine.ChangeState(broodmother.IdleState);
            return;
        }

        if (broodmother.ChangePhase())
        {
            broodmother.StateMachine.ChangeState(broodmother.RetreatingState);
            return;
        }

        broodmother.TurnToEnemy();

        if (broodmother.CompoundAttack.MakeAfficientAttack(broodmother.Enemy.transform.position) == true)
        {
            broodmother.Stop();
        }
        else
        {
            broodmother.Move();
        }
    }

    public void PhysicsUpdate()
    {
        
    }
}
