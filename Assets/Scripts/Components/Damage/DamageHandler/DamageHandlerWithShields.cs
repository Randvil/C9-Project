using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandlerWithShields : DamageHandler
{
    protected float shieldPercentageAbsorption;
    protected float shieldCrushMuliplierDamage;

    protected IHealthManager shieldManager;

    public DamageHandlerWithShields(DamageHandlerWithShieldsData damageHandlerWithShieldsData, IHealthManager healthManager, IHealthManager shieldManager, IModifierManager defenceModifierManager, IEffectManager effectManager, IDeathManager deathManager) : base(healthManager, defenceModifierManager, effectManager, deathManager)
    {
        shieldPercentageAbsorption = damageHandlerWithShieldsData.shieldPercentageAbsorption;
        shieldCrushMuliplierDamage = damageHandlerWithShieldsData.shieldCrushMuliplierDamage;

        this.shieldManager = shieldManager;
    }

    protected override void ChangeHealth(Damage incomingDamage, float effectiveDamage)
    {
        float shieldDamage = effectiveDamage * shieldPercentageAbsorption;
        float multiplier = incomingDamage.DamageType == eDamageType.ShieldCrush ? shieldCrushMuliplierDamage : 1f;

        if (shieldDamage * multiplier > shieldManager.Health.currentHealth)
        {
            shieldDamage = shieldManager.Health.currentHealth;
        }
        shieldManager.ChangeCurrentHealth(-(shieldDamage * multiplier));

        healthManager.ChangeCurrentHealth(shieldDamage - effectiveDamage);
    }
}
