using UnityEngine.Events;

public interface IMovement
{
    public void StartMove();
    public void BreakMove();
    public bool IsMoving { get; }
    public float Speed { get; }
    public float MaxSpeed { get; }
    public UnityEvent StartMoveEvent { get; }
    public UnityEvent BreakMoveEvent { get; }
}
