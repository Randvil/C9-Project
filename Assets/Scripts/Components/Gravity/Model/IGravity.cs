public interface IGravity
{
    public void Enable(object disabler);
    public void Disable(object disabler);
    public void SetFallingState();
    public bool IsDisabled { get; }
    public bool IsGrounded { get; }
    public bool IsFalling { get; }
}
