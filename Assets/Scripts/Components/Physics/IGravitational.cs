using UnityEngine.Events;

public interface IGravitational
{
    public bool IsGrounded();
    public void HandleJumpGravity();
    public void GravityWhileFalling(float FallMultiplier);
    public void DisableGravity();
    public void EnableGravity();
    public void HandleClimbGravity();
    public UnityEvent<int> GravityFallEvent { get; }
    public eJumpState FallState { get; }
}
