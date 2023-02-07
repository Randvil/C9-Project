using UnityEngine.Events;

public interface IGravitational
{
    public bool IsGrounded();
    public void HandleJumpGravity();
    public void GravityWhileFalling(float FallMultiplier);
    public void DisableGravity();
    public void EnableGravity();
    public void HandleClimbGravity();
    public UnityEvent<bool> GravityFallEvent { get; }
    public UnityEvent<float> GravityFallStateEvent { get; }
    public eJumpState FallState { get; }
}
