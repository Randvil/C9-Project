using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderboyCompoundProtection : ICompoundProtection
{
    private GameObject spiderboy;

    private IHealthManager healthManager;
    private IAbility jump;
    private IEffectManager effectManager;
    private IDeathManager deathManager;

    private bool isDisabled;
    private float relativeHealthThreshold;
    private float enemyDistanceThreshold;

    public bool CanUseProtection => jump.CanBeUsed;

    public SpiderboyCompoundProtection(GameObject spiderboy, CompoundProtectionData compoundProtectionData, IHealthManager healthManager, IAbility jump, IEffectManager effectManager, IDeathManager deathManager)
    {
        this.spiderboy = spiderboy;

        relativeHealthThreshold = compoundProtectionData.relativeHealthThreshold;
        enemyDistanceThreshold = compoundProtectionData.enemyDistanceThreshold;

        this.healthManager = healthManager;
        this.jump = jump;
        this.effectManager = effectManager;
        this.deathManager = deathManager;

        effectManager.EffectEvent.AddListener(OnStun);
        deathManager.DeathEvent.AddListener(OnDeath);
    }

    public void Protect(Vector2 enemyPosition)
    {
        if (isDisabled)
        {
            return;
        }

        if (Vector2.Distance(enemyPosition, spiderboy.transform.position) < enemyDistanceThreshold && healthManager.Health.currentHealth / healthManager.Health.maxHealth <= relativeHealthThreshold)
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

    public void OnDeath()
    {
        isDisabled = true;
    }
}
