using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerClimb : IClimb
{
    public bool CanClimb { get; }
    public bool HaveToTurn { get; }
}
