using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ReflectableProjectile
{
    [SerializeField] private BombData bombData;
    private IGravity gravity;
    private Explosion explosion;

    public float ExplosionRadius { get; protected set; }

    private void Awake()
    {
        gravity = GetComponent<IGravity>();
        gravity.Disable(this);
    }

    public override void Release(GameObject projectileOwner, DamageData damageData, eDirection direction, ITeam ownerTeam, IModifierManager modifierManager, IDamageDealer damageDealer)
    {
        base.Release(projectileOwner, damageData, direction, ownerTeam, modifierManager, damageDealer);

        ExplosionRadius = bombData.explosionRadius;
        explosion = new Explosion(ExplosionRadius, damage, ownerTeam, damageDealer);

        gravity.Enable(this);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (ownerTeam.IsSame(other))
        {
            return;
        }

        explosion.Explode(transform.position);
        Remove();
    }
}
