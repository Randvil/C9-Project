using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolmanPatrollingState : IState
{
    private PatrolmanStrategy patrolman;

    public PatrolmanPatrollingState(PatrolmanStrategy patrolmanStrategy)
    {
        this.patrolman = patrolmanStrategy;
    }

    public void Enter()
    {
        patrolman.Move();
    }

    public void Exit()
    {
        patrolman.Stop();
    }

    public void LogicUpdate()
    {
        patrolman.SearchEnemy();
        if (patrolman.Enemy != null)
        {
            patrolman.StateMachine.ChangeState(patrolman.AttackingState);
        }
    }

    public void PhysicsUpdate()
    {
        patrolman.CheckPlatformAhead();
        patrolman.CheckWallAhead();
        if (patrolman.PlatformIsAhead == false || patrolman.WallIsAhead == true)
        {
            patrolman.Turn();
            patrolman.Move();
        }
    }
}
