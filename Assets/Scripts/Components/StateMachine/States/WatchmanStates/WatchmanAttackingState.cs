using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchmanAttackingState : IState
{
    private WatchmanStrategy watchman;

    public WatchmanAttackingState(WatchmanStrategy watchman)
    {
        this.watchman = watchman;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        watchman.CompoundAttack.BreakAttack();
    }

    public void LogicUpdate()
    {
        if (watchman.EnemyIsTracking() == false)
        {
            watchman.StateMachine.ChangeState(watchman.IdleState);
            return;
        }

        watchman.TurnToEnemy();

        watchman.CompoundAttack.MakeAfficientAttack(watchman.Enemy.transform.position);
    }

    public void PhysicsUpdate()
    {
        
    }
}
