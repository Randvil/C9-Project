using UnityEngine.Events;

public interface IPlayerInput
{
    public UnityEvent<eDirection> MoveEvent { get; }
    public UnityEvent StopEvent { get; }
    public UnityEvent JumpEvent { get; }
    public UnityEvent AttackEvent { get; }
    public UnityEvent RollEvent { get; }
    public UnityEvent<eAbilityType> AbilityEvent { get; }
}
