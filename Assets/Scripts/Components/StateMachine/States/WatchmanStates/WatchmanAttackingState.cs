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
        watchman.CompoundProtection.BreakProtection();
    }

    public void LogicUpdate()
    {
        if (watchman.EnemyIsTracking() == false)
        {
            watchman.StateMachine.ChangeState(watchman.IdleState);
            return;
        }

        watchman.TurnToEnemy();

        if (watchman.CompoundProtection.CanUseProtection)
        {
            watchman.CompoundProtection.Protect(watchman.Enemy.transform.position);
        }

        if (watchman.CompoundAttack.IsPerforming)
        {
            return;
        }       
                
        watchman.CompoundAttack.MakeAfficientAttack(watchman.Enemy.transform.position);
    }

    public void PhysicsUpdate()
    {
        
    }
}
