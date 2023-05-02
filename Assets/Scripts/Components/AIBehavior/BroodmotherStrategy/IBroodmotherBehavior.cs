using System;
using UnityEngine;

public interface IBroodmotherBehavior
{
    public BroodmotherStrategyData BroodmotherStrategyData { get; }
    public BoxCollider2D Collider { get; }
    public ITeam CharacterTeam { get; }
    public IHealthManager HealthManager { get; }
    public IHealthManager ShieldManager { get; }
    public IDamageHandler DamageHandler { get; }
    public IEffectManager EffectManager { get; }
    public IDeathManager DeathManager { get; }
    public ITurning Turning { get; }
    public IMovement Movement { get; }
    public IClimb Climb { get; }
    public ICompoundAttack CompoundAttack { get; }
    public IAbility RegenerationAbility { get; }
}
