using UnityEngine;
using UnityEngine.Events;

public interface IProjectile
{
    public void Release(GameObject projectileOwner, DamageData damageData, eDirection direction, ITeam ownerTeam, IModifierManager modifierManager, IDamageDealer damageDealer);
    public void Remove();
    public UnityEvent SpawnEvent { get; }
    public UnityEvent ExtinctionEvent { get;}
}
