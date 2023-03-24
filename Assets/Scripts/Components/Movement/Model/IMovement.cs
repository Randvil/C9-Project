public interface IMovement
{
    public void StartMove();
    public void StopMove();
    public bool IsMoving { get; }
    public float Speed { get; }
}
