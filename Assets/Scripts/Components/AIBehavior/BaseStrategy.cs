using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStrategy : IAIBehavior
{
    protected MonoBehaviour owner;
    protected float searchEnemyDistance;
    protected LayerMask enemyLayer;

    protected BoxCollider2D collider;
    protected ITeam characterTeam;
    protected IDamageHandler damageHandler;
    protected IEffectManager effectManager;
    protected IDeathManager deathManager;
    protected ITurning turning;

    protected float logicUpdateDelay = 0.1f;
    protected float physicsUpdateDelay = Time.fixedDeltaTime;
    protected Coroutine logicUpdateCoroutine;
    protected Coroutine physicsUpdateCoroutine;

    public IStateMachine StateMachine { get; protected set; }
    public IState IdleState { get; protected set; }
    public IState AttackingState { get; protected set; }
    public IState StunnedState { get; protected set; }
    public IState DyingState { get; protected set; }

    public GameObject Enemy { get; protected set; }
    public ICompoundAttack CompoundAttack { get; protected set; }

    public BaseStrategy(MonoBehaviour owner)
    {
        this.owner = owner;
    }

    public virtual void Activate()
    {
        damageHandler.TakeDamageEvent.AddListener(OnTakeDamage);
        effectManager.EffectEvent.AddListener(OnStun);
        deathManager.DeathEvent.AddListener(OnDie);

        if (StateMachine.CurrentState == null)
        {
            StateMachine.Initialize(IdleState);
        }
        else
        {
            StateMachine.ChangeState(IdleState);
        }        

        if (logicUpdateCoroutine == null)
        {
            logicUpdateCoroutine = owner.StartCoroutine(LogicUpdateCoroutine());
        }
        if (physicsUpdateCoroutine == null)
        {
            physicsUpdateCoroutine = owner.StartCoroutine(PhysicsUpdateCoroutine());
        }
    }

    public virtual void Deactivate()
    {
        damageHandler.TakeDamageEvent.RemoveListener(OnTakeDamage);
        effectManager.EffectEvent.RemoveListener(OnStun);
        deathManager.DeathEvent.RemoveListener(OnDie);

        if (logicUpdateCoroutine != null)
        {
            owner.StopCoroutine(logicUpdateCoroutine);
            logicUpdateCoroutine = null;
        }
        if (physicsUpdateCoroutine != null)
        {
            owner.StopCoroutine(physicsUpdateCoroutine);
            physicsUpdateCoroutine = null;
        }
    }

    protected IEnumerator LogicUpdateCoroutine()
    {
        while (true)
        {
            StateMachine.CurrentState.LogicUpdate();
            yield return new WaitForSeconds(logicUpdateDelay);
        }
    }

    protected IEnumerator PhysicsUpdateCoroutine()
    {
        while (true)
        {
            StateMachine.CurrentState.PhysicsUpdate();
            yield return new WaitForSeconds(physicsUpdateDelay);
        }
    }

    public virtual void SearchEnemy()
    {
        Vector2 direction = turning.Direction == eDirection.Left ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(collider.transform.position, direction, searchEnemyDistance, enemyLayer);

        Collider2D enemy = hit.collider;

        if (enemy == null)
        {
            return;
        }

        if (characterTeam.IsSame(enemy))
        {
            return;
        }

        Enemy = enemy.gameObject;
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
            LoseSightOfEnemy();
        }

        return enemyIsNear;
    }

    public virtual void LoseSightOfEnemy()
    {
        Enemy = null;
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
        if (Enemy == null)
        {
            return;
        }

        TurnToPoint(Enemy.transform.position.x);
    }

    protected virtual void OnTakeDamage(DamageInfo damageInfo)
    {
        GameObject damagingEnemy = damageInfo.damageSourceObject;

        if (damagingEnemy == null)
        {
            return;
        }

        if (characterTeam.IsSame(damagingEnemy))
        {
            return;
        }

        if (Enemy == null)
        {
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
