using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderboyCompoundProtection : ICompoundProtection
{
    private IHealthManager healthManager;
    private IAbility jump;
    private IEffectManager effectManager;

    private bool isDisabled;
    private float relativeHealthThreshold;

    public SpiderboyCompoundProtection(CompoundProtectionData compoundProtectionData, IHealthManager healthManager, IAbility jump, IEffectManager effectManager)
    {
        relativeHealthThreshold = compoundProtectionData.relativeHealthThreshold;

        this.healthManager = healthManager;
        this.jump = jump;
        this.effectManager = effectManager;

        effectManager.EffectEvent.AddListener(OnStun);
    }

    public void Protect()
    {
        if (isDisabled)
        {
            return;
        }

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

    public void OnStun(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType != eEffectType.Stun)
        {
            return;
        }

        if (effectStatus == eEffectStatus.Added)
        {
            isDisabled = true;
        }

        if (effectStatus == eEffectStatus.Cleared)
        {
            isDisabled = false;
        }
    }
}
