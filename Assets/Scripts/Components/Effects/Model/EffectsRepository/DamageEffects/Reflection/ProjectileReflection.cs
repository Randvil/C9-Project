using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileReflection : BaseEffect, IDamageEffect
{
    protected ITeam team;

    public UnityEvent DamageEffectEvent { get; } = new();

    public ProjectileReflection(EffectData effectData, ITeam team) : base(effectData)
    {
        this.team = team;
    }

    public virtual void ApplyEffect(Damage incomingDamage)
    {
        if (incomingDamage.SourceObject.TryGetComponent(out IReflectableProjectile projectile))
        {
            projectile.CreateReflectedProjectile(team);
            DamageEffectEvent.Invoke();
        }
    }
}
