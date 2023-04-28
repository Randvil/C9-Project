using UnityEngine;
using UnityEngine.Events;

public class Turning : ITurning
{
    public eDirection Direction { get; private set; }
    public UnityEvent<eDirection> TurnEvent { get; private set; } = new();

    public void Turn(eDirection direction)
    {
        Direction = direction;
        TurnEvent.Invoke(direction);
    }
}
