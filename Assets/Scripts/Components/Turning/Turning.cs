using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turning : MonoBehaviour, ITurning
{
    private eDirection direction;
    public eDirection Direction { get => direction; }

    public UnityEvent<eDirection> TurnEvent { get; } = new();

    public void Turn(eDirection direction)
    {
        this.direction = direction;

        TurnEvent.Invoke(this.direction);
    }
}
