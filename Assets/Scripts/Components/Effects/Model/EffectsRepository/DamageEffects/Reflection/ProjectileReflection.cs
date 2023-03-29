using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileReflection : IDamageEffect
{
    protected ITeam team;

    public float EndEffectTime { get; private set; }

    public UnityEvent DamageEffectEvent { get; } = new();

    public ProjectileReflection(ITeam team, float endEffectTime)
    {
        this.team = team;
        EndEffectTime = endEffectTime;
    }

    public virtual void ApplyEffect(Damage incomingDamage)
    {
        IReflectableProjectile projectile = incomingDamage.SourceObject.GetComponent<IReflectableProjectile>();

        if (projectile != null)
        {
            projectile.CreateReflectedProjectile(team);
            DamageEffectEvent.Invoke();
        }            
    }
}
