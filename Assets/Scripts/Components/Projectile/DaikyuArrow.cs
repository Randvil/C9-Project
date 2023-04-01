using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaikyuArrow : Projectile
{
    [SerializeField]
    protected DaikyuEffectsData daikyuEffectsData;

    protected Damage doTDamage;
    protected List<Collider2D> hitEnemies = new();

    protected override void Start()
    {
        base.Start();
        doTDamage = new(projectileOwner, gameObject, daikyuEffectsData.doTDamageData, modifierManager);
    }


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (hitEnemies.Contains(other))
        {
            return;
        }            

        hitEnemies.Add(other);

        if (other.TryGetComponent(out ITeam hitCreature) == false || hitCreature.Team == ownerTeam.Team)
        {
            return;
        }
        
        if (other.TryGetComponent(out IDamageable damageableEnemy) == true)
        {
            damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer.DealDamageEvent);
        }
        
        if (other.TryGetComponent(out IEffectable effectableEnemy) == true)
        {
            effectableEnemy.EffectManager.AddEffect(new DoTEffect(doTDamage, daikyuEffectsData.damagePeriod, Time.time + daikyuEffectsData.doTDuration, damageableEnemy.DamageHandler, damageDealer));
            effectableEnemy.EffectManager.AddEffect(new SlowEffect(daikyuEffectsData.movementSlow, Time.time + daikyuEffectsData.slowDuration));
        }
    }
}
