using UnityEngine.Events;

public interface IParry
{
    public void StartParry();
    public void BreakParry();
    public bool IsParrying { get; }
    public bool IsOnCooldown { get; }
    public bool CanParry { get; }
    public UnityEvent ParryEvent { get; }
}
