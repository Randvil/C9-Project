using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchmanWatchingState : IState
{
    private WatchmanStrategy watchman;
    private float turningTimePeriod;

    private float nextTurningTime;

    public WatchmanWatchingState(WatchmanStrategy watchman)
    {
        this.watchman = watchman;
        turningTimePeriod = watchman.TurningTimePeriod;
    }

    public void Enter()
    {
        SetNextTurnTime();
    }

    public void Exit()
    {
        
    }

    public void LogicUpdate()
    {
        watchman.SearchEnemy();
        if (watchman.Enemy != null)
        {
            watchman.StateMachine.ChangeState(watchman.AttackingState);
            return;
        }

        if (Time.time > nextTurningTime)
        {
            watchman.Turn();
            SetNextTurnTime();
        }
    }

    public void PhysicsUpdate()
    {
        
    }

    private void SetNextTurnTime()
    {
        nextTurningTime = Time.time + turningTimePeriod;
    }
}
