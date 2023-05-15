using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICompoundProtection 
{
    public void Protect(Vector2 enemyPosition);
    public void BreakProtection();
    public bool CanUseProtection { get; }
}
