using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolmanAttackingState : IState
{
    private PatrolmanStrategy patrolman;

    public PatrolmanAttackingState(PatrolmanStrategy patrolman)
    {
        this.patrolman = patrolman;
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        patrolman.CompoundAttack.BreakAttack();
        patrolman.Stop();
    }

    public void LogicUpdate()
    {
        if (patrolman.EnemyIsTracking() == false)
        {
            patrolman.StateMachine.ChangeState(patrolman.IdleState);
            return;
        }

        patrolman.TurnToEnemy();

        if (patrolman.CompoundAttack.IsPerforming)
        {
            return;
        }

        if (patrolman.CompoundAttack.MakeAfficientAttack(patrolman.Enemy.transform.position) == true)
        {
            patrolman.Stop();
            return;
        }
        else
        {
            patrolman.Move();
        }

        patrolman.CheckPlatformAhead();
        patrolman.CheckWallAhead();
        if (patrolman.PlatformIsAhead == false || patrolman.WallIsAhead == true)
        {
            patrolman.LoseSightOfEnemy();
            patrolman.Turn();
            patrolman.StateMachine.ChangeState(patrolman.IdleState);
        }
    }

    public void PhysicsUpdate()
    {

    }
}
