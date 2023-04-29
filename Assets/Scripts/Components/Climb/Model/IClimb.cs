using UnityEngine.Events;

public interface IClimb 
{
    public void StartClimb();
    public void BreakClimb();
    public void ClimbUp();
    public void ClimbDown();
    public void StopClimb();
    public bool IsClimbing { get; }
    public float ClimbSpeed { get; }
    public UnityEvent StartClimbEvent { get; }
    public UnityEvent BreakClimbEvent { get; }
    public UnityEvent ClimbStateChangedEvent { get; }
}
