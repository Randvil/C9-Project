using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodmotherRetreatingState : IState
{
    private BroodmotherStrategy broodmother;

    public BroodmotherRetreatingState(BroodmotherStrategy broodmother)
    {
        this.broodmother = broodmother;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        broodmother.BreakRetreat();
    }

    public void LogicUpdate()
    {
        if (broodmother.Retreat() == false)
        {
            broodmother.StateMachine.ChangeState(broodmother.AttackingState);
        }
    }

    public void PhysicsUpdate()
    {
        
    }
}
