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
        if (Time.time > 1f)
        {
            broodmother.StateMachine.ChangeState(broodmother.AttackingState);
        }
        Debug.Log("Doing Nothing");
    }

    public void PhysicsUpdate()
    {
        
    }
}
