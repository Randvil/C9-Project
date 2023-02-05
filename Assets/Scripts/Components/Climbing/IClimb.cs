using UnityEngine.Events;
using UnityEngine;

public interface IClimb 
{
    public eClimbState ClimbState { get; }
    public float Speed { get; set; }
    public void HandleClimb(int dir, Ladder ladder);
    public void MoveToLadder(Ladder ladder);
    public void UpdateClimbingState();
    public bool IsClimbing { get; }
    public UnityEvent<bool> EntityClimbEvent { get; }
    public UnityEvent<float, float> EntityClimbStateEvent { get; }

}
