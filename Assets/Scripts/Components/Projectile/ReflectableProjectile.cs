using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectableProjectile : Projectile, IReflectableProjectile
{
    public virtual void CreateReflectedProjectile(ITeam newTeam)
    {
        eDirection newDirection = direction == eDirection.Left ? eDirection.Right : eDirection.Left;

        GameObject reflectedProjectile = Instantiate(gameObject);
        reflectedProjectile.GetComponent<IReflectableProjectile>().Release(projectileOwner, damageData, newDirection, newTeam, modifierManager, damageDealer);

        Remove();
    }
}
