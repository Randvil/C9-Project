using UnityEngine;
using UnityEngine.Events;

public interface IEnemyBehavior
{
    public void Activate();
    public void Deactivate();
    public UnityEvent<eDirection> DirectionalMoveEvent { get; }
    public UnityEvent<eDirection> TurnEvent { get; }
    public UnityEvent<Vector2> FreeMoveEvent { get; }
    public UnityEvent StopEvent { get; }
    public UnityEvent<Vector2> JumpEvent { get; }
    public UnityEvent AttackEvent { get; }
    public UnityEvent<eAbilityType> AbilityEvent { get; }
}
