using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaikyuArrow : Projectile
{
    [SerializeField] protected DoTEffectData doTEffectData;
    [SerializeField] protected SlowEffectData slowEffectData;

    protected List<Collider2D> hitEnemies = new();

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (hitEnemies.Contains(other))
        {
            return;
        }            

        hitEnemies.Add(other);

        if (ownerTeam.IsSame(other))
        {
            return;
        }
        
        if (other.TryGetComponent(out IDamageable damageableEnemy) == true)
        {
            damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer);
        }
        
        if (other.TryGetComponent(out IEffectable effectableEnemy) == true)
        {
            effectableEnemy.EffectManager.AddEffect(new DoTEffect(projectileOwner, gameObject, doTEffectData, modifierManager, damageableEnemy.DamageHandler, damageDealer));
            effectableEnemy.EffectManager.AddEffect(new SlowEffect(slowEffectData));
        }
    }
}
