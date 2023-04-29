using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolmanStrategy : BaseStrategy
{
    private Transform checkPlatformAheadTransform;

    private PatrolmanStrategyData patrolmanStrategyData;
    private float checkPlatformAheadRadius;
    private float checkWallDistance;
    private LayerMask platformLayer;

    private IMovement movement;

    public bool PlatformIsAhead { get; private set; }
    public bool WallIsAhead { get; private set; }

    public PatrolmanStrategy(IPatrollingBehavior patrolling)
    {
        checkPlatformAheadTransform = patrolling.CheckPlatformAheadTransform;

        patrolmanStrategyData = patrolling.PatrolmanStrategyData;
        searchEnemyDistance = patrolmanStrategyData.searchEnemyDistance;
        checkPlatformAheadRadius = patrolmanStrategyData.checkPlatformAheadRadius;
        checkWallDistance = patrolmanStrategyData.checkWallDistance;
        enemyLayer = patrolmanStrategyData.enemyLayer;
        platformLayer = patrolmanStrategyData.platformLayer;

        collider = patrolling.Collider;
        characterTeam = patrolling.CharacterTeam;
        damageHandler = patrolling.DamageHandler;
        effectManager = patrolling.EffectManager;
        deathManager = patrolling.DeathManager;
        turning = patrolling.Turning;
        movement = patrolling.Movement;
        CompoundAttack = patrolling.CompoundAttack;

        StateMachine = new StateMachine();
        IdleState = new PatrolmanPatrollingState(this);
        AttackingState = new PatrolmanAttackingState(this);
        StunnedState = new PatrolmanStunnedState();
        DyingState = new PatrolmanDyingState();
    }

    public void CheckPlatformAhead()
    {
        PlatformIsAhead = Physics2D.OverlapCircle(checkPlatformAheadTransform.position, checkPlatformAheadRadius, platformLayer) != null;
    }

    public void CheckWallAhead()
    {
        float offset = (collider.size.x * collider.transform.lossyScale.x + checkWallDistance) / 2f;
        if (turning.Direction == eDirection.Left)
        {
            offset = -offset;
        }

        Vector2 boxOrigin = new(collider.transform.position.x + offset, collider.transform.position.y);

        Vector2 boxSize = new(checkWallDistance / 2f, collider.size.y * collider.transform.lossyScale.y * 0.9f);

        WallIsAhead = Physics2D.OverlapBox(boxOrigin, boxSize, 0f, platformLayer) != null;
    }

    public void Move()
    {
        movement.StartMove();
    }

    public void Stop()
    {
        if (movement.IsMoving)
        {
            movement.BreakMove();
        }
    }
}