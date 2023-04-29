using UnityEngine.Events;

public interface IDeathManager
{
    public void Resurrect();
    public bool IsAlive { get; }
    public UnityEvent DeathEvent { get; }
    public UnityEvent ResurrectionEvent { get; }
}
