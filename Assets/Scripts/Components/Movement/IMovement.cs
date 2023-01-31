using UnityEngine.Events;

public interface IMovement
{
    public float Speed { get; set; }
    public void StartMove(eDirection direction);
    public void StopMove();
    public bool IsMoving { get; }
    public UnityEvent<int> EntityMoveEvent { get; }
}
