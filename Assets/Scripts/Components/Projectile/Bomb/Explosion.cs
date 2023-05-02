using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosion
{
    private float radius;
    private ITeam ownerTeam;
    private Damage damage;
    private IDamageDealer damageDealer;

    public Explosion(float radius, Damage damage, ITeam ownerTeam, IDamageDealer damageDealer)
    {
        this.radius = radius;
        this.ownerTeam = ownerTeam;
        this.damage = damage;
        this.damageDealer = damageDealer;
    }

    public void Explode(Vector2 position)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(position, radius);

        foreach (Collider2D enemy in enemies)
        {
            if (ownerTeam.IsSame(enemy))
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                damageableEnemy.DamageHandler.TakeDamage(damage, damageDealer);
            }
        }
    }
}
