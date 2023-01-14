using UnityEngine.Events;

public interface IJump
{
    public void StartJump();
    public void StopJump();
    public bool IsJumping { get; }
    public UnityEvent StartJumpEvent { get; }
    public UnityEvent StopJumpEvent { get; }
}
