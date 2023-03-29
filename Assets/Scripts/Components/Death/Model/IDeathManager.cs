using UnityEngine.Events;

public interface IDeathManager
{
    public bool IsAlive { get; }
    public UnityEvent DeathEvent { get; }
}
