using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIBehavior
{
    public void Activate();
    public void Deactivate();
    public void LogicUpdate();
    public void PhysicsUpdate();
}