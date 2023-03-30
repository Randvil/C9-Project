using UnityEngine;
using UnityEngine.Events;

public interface IProjectile
{
    public void Initialize(GameObject projectileOwner, DamageData damageData, ProjectileData projectileData, eDirection direction, ITeam ownerTeam, IModifierManager modifierManager);
    public void Remove();
    public UnityEvent SpawnEvent { get; }
    public UnityEvent ExtinctionEvent { get;}
}
