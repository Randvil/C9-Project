using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractWeapon : MonoBehaviour, IWeapon
{
    [SerializeField]
    protected Damage damage;
    public Damage Damage { get => damage; }

    [SerializeField]
    protected float attackRadius;
    public float AttackRadius { get => attackRadius; }

    [SerializeField]
    protected float attackDelay;

    [SerializeField]
    protected float attackCooldown;

    public bool IsAttacking { get => attackCoroutine != null; }
    public UnityEvent StartAttackEvent { get; } = new();
    public UnityEvent ReleaseAttackEvent { get; } = new();
    public UnityEvent StopAttackEvent { get; } = new();

    protected Coroutine attackCoroutine;

    public void StartAttack(eDirection direction)
    {
        if (attackCoroutine != null)
            return;

        attackCoroutine = StartCoroutine(AttackCoroutine(direction));
    }

    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            FinishAttack();
            attackCoroutine = null;
        }
    }

    protected IEnumerator AttackCoroutine(eDirection direction)
    {
        PrepareAttack();

        if (attackDelay > 0f)
            yield return new WaitForSeconds(attackDelay);

        ReleaseAttack(direction);

        float finishAttackDelay = attackCooldown - attackDelay;
        if (finishAttackDelay > 0f)
            yield return new WaitForSeconds(finishAttackDelay);

        FinishAttack();        
    }

    protected virtual void PrepareAttack()
    {
        StartAttackEvent.Invoke();
    }

    protected virtual void FinishAttack()
    {
        StopAttackEvent.Invoke();

        attackCoroutine = null;
    }

    protected abstract void ReleaseAttack(eDirection direction);
}
