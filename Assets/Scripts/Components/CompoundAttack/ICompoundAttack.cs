using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompoundAttack
{
    public bool MakeAfficientAttack(Vector2 enemyPosition);
    public void BreakAttack();
    public bool IsPerforming { get; }
}
