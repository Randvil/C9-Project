using UnityEngine.Events;

public interface IJump
{
    public void StartJump();
    public void BreakJump();
    public bool IsJumping { get; }
    public bool CanJump { get; }
    public UnityEvent StartJumpEvent { get; }
    public UnityEvent BreakJumpEvent { get; }
}
