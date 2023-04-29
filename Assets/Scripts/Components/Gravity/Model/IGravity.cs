using UnityEngine.Events;

public interface IGravity
{
    public void Enable(object disabler);
    public void Disable(object disabler);
    public void SetFallingState();
    public bool IsDisabled { get; }
    public bool IsGrounded { get; }
    public bool IsFalling { get; }
    public UnityEvent StartFallEvent { get; }
    public UnityEvent BreakFallEvent { get; }
    public UnityEvent GroundedEvent { get; }
}
