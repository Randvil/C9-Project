using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandlerWithShields : DamageHandler
{
    protected float shieldPercentageAbsorption;

    protected IHealthManager shieldManager;

    public DamageHandlerWithShields(DamageHandlerWithShieldsData damageHandlerWithShieldsData, IHealthManager healthManager, IHealthManager shieldManager, IModifierManager defenceModifierManager, IEffectManager effectManager, IDeathManager deathManager) : base(healthManager, defenceModifierManager, effectManager, deathManager)
    {
        shieldPercentageAbsorption = damageHandlerWithShieldsData.shieldPercentageAbsorption;

        this.shieldManager = shieldManager;
    }

    protected override void ChangeHealth(float effectiveDamage)
    {
        float shieldDamage = effectiveDamage * shieldPercentageAbsorption;

        if (shieldDamage > shieldManager.Health.currentHealth)
        {
            shieldDamage = shieldManager.Health.currentHealth;
        }
        shieldManager.ChangeCurrentHealth(-shieldDamage);

        healthManager.ChangeCurrentHealth(shieldDamage - effectiveDamage);
    }
}
