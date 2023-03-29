using UnityEngine.Events;

public interface IAbility
{
    public void StartCast();
    public void BreakCast();
    public bool CanBeUsed { get; }
    public bool IsPerforming { get; }
    public UnityEvent ReleaseCastEvent { get; }
}
