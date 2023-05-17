using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherIdleState : IState
{
    private BroodmotherStrategy broodmother;

    public BroodmotherIdleState(BroodmotherStrategy broodmother)
    {
        this.broodmother = broodmother;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void LogicUpdate()
    {
        if (Time.timeSinceLevelLoad > 1f)
        {
            broodmother.StateMachine.ChangeState(broodmother.AttackingState);
        }
    }

    public void PhysicsUpdate()
    {
        
    }
}
