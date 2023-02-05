using UnityEngine.Events;

public interface IGravity
{
    public void Enable();
    public void Disable();
    public UnityEvent EnableGravityEvent { get; }
    public UnityEvent DisableGravityEvent { get; }
}
