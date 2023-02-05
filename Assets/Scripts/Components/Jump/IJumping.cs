using UnityEngine.Events;

public interface IJumping 
{
    public eJumpState jumpState { get; }
    public bool IsJumping { get; }
    public void HandleJump();
    public UnityEvent<bool> EntityJumpEvent { get; }
    public UnityEvent<float> EntityJumpStateEvent { get; }
}
