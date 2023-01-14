using UnityEngine.Events;

public interface IProjectile
{
    public void Initialize(eDirection direction, float rotation, float speed, eTeam ownerTeam, Damage damage);
    public void Remove();
    public UnityEvent SpawnEvent { get; }
    public UnityEvent ExtinctionEvent { get;}
}
