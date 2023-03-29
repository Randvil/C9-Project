public interface IRoll
{
    public void StartRoll();
    public void BreakRoll();
    public bool IsRolling { get; }
    public bool IsOnCooldown { get; }
    public bool CanRoll { get; }
    public float RollDuration { get; }
}
