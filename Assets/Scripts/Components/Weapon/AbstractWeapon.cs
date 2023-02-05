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
    protected float[] preAttackDelays;

    [SerializeField]
    protected float postAttackDelay;

    public bool IsAttacking { get => attackCoroutine != null; }
    public UnityEvent StartAttackEvent { get; } = new();
    public UnityEvent ReleaseAttackEvent { get; } = new();
    public UnityEvent StopAttackEvent { get; } = new();

    protected Coroutine attackCoroutine;
    public int attackSeries;

    protected virtual void Awake()
    {
        damage.sourceObject = gameObject;
        damage.sourceWeapon = this;
        damage.modificators = new();
    }

    public virtual void StartAttack()
    {
        attackSeries++;

        if (attackCoroutine == null)
            attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    public virtual void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            FinishAttack();
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        PrepareAttack();

        for (int attackNumber = 1; attackNumber <= Mathf.Min(attackSeries, preAttackDelays.Length); attackNumber++)
        {
            yield return new WaitForSeconds(preAttackDelays[attackNumber - 1]);
            ReleaseAttack();
            ReleaseAttackEvent.Invoke();
        }

        yield return new WaitForSeconds(postAttackDelay);
        FinishAttack();
    }

    protected virtual void PrepareAttack()
    {
        StartAttackEvent.Invoke();
    }

    protected virtual void FinishAttack()
    {
        StopAttackEvent.Invoke();
        attackSeries = 0;
        attackCoroutine = null;
    }

    protected abstract void ReleaseAttack();
}
