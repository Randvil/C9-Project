using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderboyCompoundProtection : ICompoundProtection
{
    private IHealthManager healthManager;
    private IAbility jump;

    private float relativeHealthThreshold;

    public SpiderboyCompoundProtection(CompoundProtectionData compoundProtectionData, IHealthManager healthManager, IAbility jump)
    {
        relativeHealthThreshold = compoundProtectionData.relativeHealthThreshold;

        this.healthManager = healthManager;
        this.jump = jump;
    }

    public void Protect()
    {
        if (jump.IsPerforming)
        {
            return;
        }

        if (jump.CanBeUsed && healthManager.Health.currentHealth / healthManager.Health.maxHealth < relativeHealthThreshold)
        {
            jump.StartCast();
            return;
        }
    }

    public void BreakProtection()
    {
        if (jump.IsPerforming)
        {
            jump.BreakCast();
        }
    }
}
