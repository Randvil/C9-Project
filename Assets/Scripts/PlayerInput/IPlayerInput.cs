using UnityEngine.Events;
using UnityEngine;

public interface IPlayerInput
{
    public UnityEvent<eDirection> MoveEvent { get; }
    public UnityEvent StopEvent { get; }
    public UnityEvent JumpEvent { get; }
    public UnityEvent AttackEvent { get; }
    public UnityEvent RollEvent { get; }
    public UnityEvent<eAbilityType> AbilityEvent { get; }
    public UnityEvent ParryEvent { get; }
    public UnityEvent InteractEvent { get; }
    public UnityEvent<int> ClimbEvent { get; }
}
