using UnityEngine;
using UnityEngine.Events;

public interface IProjectile
{
    public void Initialize(eDirection direction, float zRotation, float speed, eTeam ownerTeam, Damage damage);
    public void Initialize(Vector2 velocityVector, Quaternion rotation, eTeam ownerTeam, Damage damage);
    public void Remove();
    public UnityEvent SpawnEvent { get; }
    public UnityEvent ExtinctionEvent { get;}
}
