using UnityEngine;

public interface IWatchmanBehavior
{
    public WatchmanStrategyData WatchmanStrategyData { get; }
    public BoxCollider2D Collider { get; }
    public ITeam CharacterTeam { get; }
    public IDamageHandler DamageHandler { get; }
    public IEffectManager EffectManager { get; }
    public IDeathManager DeathManager { get; }
    public ITurning Turning { get; }
    public ICompoundAttack CompoundAttack { get; }
    public ICompoundProtection CompoundProtection { get; }
}
