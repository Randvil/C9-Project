using UnityEngine.Events;

public interface IPlayerInput
{
    public UnityEvent<eDirection> MoveEvent { get; }
    public UnityEvent StopEvent { get; }
    public UnityEvent<eActionPhase> JumpEvent { get; }
    public UnityEvent<eActionPhase> AttackEvent { get; }
    public UnityEvent<eActionPhase> RollEvent { get; }
    public UnityEvent<eActionPhase, eAbilityType> AbilityEvent { get; }
    public UnityEvent<eActionPhase> ParryEvent { get; }
}
