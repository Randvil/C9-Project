using UnityEngine;

public interface IPatrollingBehavior
{
    public Transform CheckPlatformRightTransform { get; }
    public Transform CheckPlatformLeftTransform { get; }
    public PatrolmanStrategyData PatrolmanStrategyData { get; }
    public BoxCollider2D Collider { get; }
    public ITeam CharacterTeam { get; }
    public IDamageHandler DamageHandler { get; }
    public IEffectManager EffectManager { get; }
    public IDeathManager DeathManager { get; }
    public ITurning Turning { get; }
    public IMovement Movement { get; }
    public ICompoundAttack CompoundAttack { get; }
}
