using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStrategy : IAIBehavior
{
    protected float searchEnemyDistance;
    protected LayerMask enemyLayer;

    protected BoxCollider2D collider;
    protected ITeam characterTeam;
    protected IDamageHandler damageHandler;
    protected IEffectManager effectManager;
    protected IDeathManager deathManager;
    protected ITurning turning;

    public IStateMachine StateMachine { get; protected set; }
    public IState IdleState { get; protected set; }
    public IState AttackingState { get; protected set; }
    public IState StunnedState { get; protected set; }
    public IState DyingState { get; protected set; }

    public GameObject Enemy { get; protected set; }
    public ICompoundAttack CompoundAttack { get; protected set; }

    public virtual void Activate()
    {
        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
        effectManager.EffectEvent.AddListener(OnStun);
        deathManager.DeathEvent.AddListener(OnDie);

        if (StateMachine.CurrentState == null)
        {
            StateMachine.Initialize(IdleState);
            return;
        }

        StateMachine.ChangeState(IdleState);
    }

    public virtual void Deactivate()
    {
        damageHandler.TakeDamageEvent.RemoveListener(OnTakeDamage);
        effectManager.EffectEvent.RemoveListener(OnStun);
        deathManager.DeathEvent.RemoveListener(OnDie);

        StateMachine.ChangeState(IdleState);
    }

    public virtual void LogicUpdate()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void PhysicsUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SearchEnemy()
    {
        Vector2 direction = turning.Direction == eDirection.Left ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(collider.transform.position, direction, searchEnemyDistance, enemyLayer);
        if (hit.collider == null)
        {
            return;
        }

        if (hit.collider.TryGetComponent(out ITeamMember enemyTeam) == false || enemyTeam.CharacterTeam.Team == characterTeam.Team)
        {
            return;
        }

        Enemy = hit.collider.gameObject;
    }

    public virtual bool EnemyIsTracking()
    {
        if (Enemy == null)
        {
            return false;
        }

        bool enemyIsNear = Vector2.Distance(Enemy.transform.position, collider.transform.position) < searchEnemyDistance;

        if (enemyIsNear == false)
        {
            Enemy = null;
        }

        return enemyIsNear;
    }

    public virtual void Turn()
    {
        eDirection direction = turning.Direction == eDirection.Right ? eDirection.Left : eDirection.Right;
        turning.Turn(direction);
    }

    public virtual void TurnToPoint(float xPosition)
    {
        float relativePointPosition = xPosition - collider.transform.position.x;
        if (relativePointPosition > 0f)
        {
            turning.Turn(eDirection.Right);
        }
        else
        {
            turning.Turn(eDirection.Left);
        }
    }

    public virtual void TurnToEnemy()
    {
        TurnToPoint(Enemy.transform.position.x);
    }

    protected virtual void OnTakeDamage(DamageInfo damageInfo)
    {
        GameObject damagingEnemy = damageInfo.damageSourceObject;

        if (Enemy == null)
        {
            if (damagingEnemy.TryGetComponent(out ITeamMember damageDealerTeamMember) == false
                || damageDealerTeamMember.CharacterTeam.Team == characterTeam.Team)
            {
                return;
            }

            Enemy = damagingEnemy;
        }

        if (damagingEnemy.transform.position.x - Enemy.transform.position.x < 0f)
        {
            Enemy = damagingEnemy;
        }
    }

    protected virtual void OnStun(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType != eEffectType.Stun)
        {
            return;
        }

        if (effectStatus == eEffectStatus.Added)
        {
            StateMachine.ChangeState(StunnedState);
        }
        else if (effectStatus == eEffectStatus.Cleared)
        {
            StateMachine.ChangeState(AttackingState);
        }
    }

    protected virtual void OnDie()
    {
        StateMachine.ChangeState(DyingState);
    }
}
