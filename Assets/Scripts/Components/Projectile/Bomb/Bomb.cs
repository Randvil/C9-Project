using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    protected float explosionRadius;

    public void Initialize(BombData bombData)
    {
        explosionRadius = bombData.explosionRadius;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Explode();
            Remove();
        }
    }

    protected virtual void Explode()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out ITeamMember creatureTeam) == false || creatureTeam.CharacterTeam.Team == ownerTeam.Team)
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer.DealDamageEvent);
            }
        }
    }
}
