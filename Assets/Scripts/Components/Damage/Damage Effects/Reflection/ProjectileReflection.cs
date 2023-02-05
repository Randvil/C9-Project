using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileReflection : IDamageEffect
{
    protected ITeam team;

    public ProjectileReflection(ITeam team)
    {
        this.team = team;
    }

    public virtual void ApplyEffect(Damage incomingDamage)
    {
        IReflectableProjectile projectile = incomingDamage.sourceObject.GetComponent<IReflectableProjectile>();

        if (projectile != null)
        {
            projectile.CreateReflectedProjectile(team.Team);
        }            
    }
}
