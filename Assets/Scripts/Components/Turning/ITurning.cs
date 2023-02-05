using UnityEngine.Events;

public interface ITurning
{
    public void Turn(eDirection direction);
    public eDirection Direction { get; }
    public UnityEvent<eDirection> TurnEvent { get; }
}
