public interface IJump
{
    public void StartJump();
    public void BreakJump();
    public void UpdateJumpSpeed();
    public void CheckGround();
    public bool IsJumping { get; }
    public bool CanJump { get; }
}
