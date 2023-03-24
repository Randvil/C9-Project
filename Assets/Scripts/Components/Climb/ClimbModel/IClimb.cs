public interface IClimb 
{
    public void StartClimb();
    public void BreakClimb();
    public void ClimbUp();
    public void ClimbDown();
    public void StopClimb();
    public bool CanClimb { get; }
    public bool IsClimbing { get; }
    public bool HaveToTurn { get; }
}
