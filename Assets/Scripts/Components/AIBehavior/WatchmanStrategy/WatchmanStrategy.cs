using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchmanStrategy : BaseStrategy
{
    private WatchmanStrategyData watchmanData;

    public ICompoundProtection CompoundProtection { get; private set; }

    public float TurningTimePeriod { get; private set; }

    public WatchmanStrategy(MonoBehaviour owner, IWatchmanBehavior watchman) : base(owner)
    {
        watchmanData = watchman.WatchmanStrategyData;
        searchEnemyDistance = watchmanData.searchEnemyDistance;
        TurningTimePeriod = watchmanData.turningTimePeriod;
        enemyLayer = watchmanData.enemyLayer;
        collider = watchman.Collider;
        characterTeam = watchman.CharacterTeam;
        damageHandler = watchman.DamageHandler;
        effectManager = watchman.EffectManager;
        deathManager = watchman.DeathManager;
        turning = watchman.Turning;
        CompoundAttack = watchman.CompoundAttack;
        CompoundProtection = watchman.CompoundProtection;

        StateMachine = new StateMachine();
        IdleState = new WatchmanWatchingState(this);
        AttackingState = new WatchmanAttackingState(this);
        StunnedState = new WatchmanStunnedState();
        DyingState = new WatchmanDyingState();
    }
}
