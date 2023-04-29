using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaikyuArrow : Projectile
{
    [SerializeField]
    protected DaikyuEffectsData daikyuEffectsData;

    protected DamageData doTDamageData;
    protected float doTDuration;
    protected float damagePeriod;
    protected float movementSlow;
    protected float slowDuration;

    protected Damage doTDamage;
    protected List<Collider2D> hitEnemies = new();

    protected override void Start()
    {
        base.Start();

        doTDamageData = daikyuEffectsData.doTDamageData;
        doTDuration = daikyuEffectsData.doTDuration;
        damagePeriod = daikyuEffectsData.damagePeriod;
        movementSlow = daikyuEffectsData.movementSlow;
        slowDuration = daikyuEffectsData.slowDuration;

        doTDamage = new(projectileOwner, gameObject, doTDamageData, modifierManager);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (hitEnemies.Contains(other))
        {
            return;
        }            

        hitEnemies.Add(other);

        if (other.TryGetComponent(out ITeamMember hitCreature) == false || hitCreature.CharacterTeam.Team == ownerTeam.Team)
        {
            return;
        }
        
        if (other.TryGetComponent(out IDamageable damageableEnemy) == true)
        {
            damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer.DealDamageEvent);
        }
        
        if (other.TryGetComponent(out IEffectable effectableEnemy) == true)
        {
            effectableEnemy.EffectManager.AddEffect(new DoTEffect(doTDamage, damagePeriod, Time.time + doTDuration, damageableEnemy.DamageHandler, damageDealer));
            effectableEnemy.EffectManager.AddEffect(new SlowEffect(movementSlow, Time.time + slowDuration));
        }
    }
}
