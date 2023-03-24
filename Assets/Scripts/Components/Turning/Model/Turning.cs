using UnityEngine;

public class Turning : ITurning
{
    public eDirection Direction { get; private set; }

    public void Turn(eDirection direction)
    {
        Direction = direction;
    }
}
