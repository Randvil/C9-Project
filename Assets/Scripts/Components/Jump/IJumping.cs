using UnityEngine.Events;

public interface IJumping 
{
    public eJumpState jumpState { get; }
    public bool IsJumping { get; }
    public void HandleJump();
    public UnityEvent<int> EntityJumpEvent { get; }
}
