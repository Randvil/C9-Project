using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractWeapon : IWeapon, IDamageDealer
{
    protected MonoBehaviour owner;
    protected GameObject weaponOwnerObject;

    protected DamageData[] damageDatas;
    protected float attackRange;
    protected float[] preAttackDelays;
    protected float postAttackDelay;
    protected float attackSpeed;

    protected IModifierManager weaponModifierManager;
    protected ITeam team;
    protected ITurning turning;

    protected Coroutine attackCoroutine;
    protected int attackSeries;

    public bool IsAttacking => attackCoroutine != null;
    public float AttackSpeed => attackSpeed;
    public float AttackRange => attackRange;

    public UnityEvent StartAttackEvent { get; } = new();
    public UnityEvent BreakAttackEvent { get; } = new();
    public UnityEvent ReleaseAttackEvent { get; } = new();
    public UnityEvent<DamageInfo> DealDamageEventCallback { get; } = new();

    public AbstractWeapon(MonoBehaviour owner, GameObject weaponOwnerObject, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning)
    {
        this.owner = owner;
        this.weaponOwnerObject = weaponOwnerObject;

        damageDatas = weaponData.damageDatas;
        attackRange = weaponData.attackRange;
        preAttackDelays = weaponData.preAttackDelays;
        postAttackDelay = weaponData.postAttackDelay;
        attackSpeed = weaponData.attackSpeed;

        this.weaponModifierManager = weaponModifierManager;
        this.team = team;
        this.turning = turning;
    }

    public virtual void StartAttack()
    {
        attackSeries++;

        if (attackCoroutine == null)
        {
            attackCoroutine = owner.StartCoroutine(AttackCoroutine());

            StartAttackEvent.Invoke();
        }
    }

    public virtual void BreakAttack()
    {
        if (attackCoroutine != null)
        {
            owner.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            attackSeries = 0;

            BreakAttackEvent.Invoke();
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        for (int attackNumber = 1; attackNumber <= Mathf.Min(attackSeries, preAttackDelays.Length); attackNumber++)
        {
            yield return new WaitForSeconds(preAttackDelays[attackNumber - 1] / attackSpeed);
            ReleaseAttack(attackNumber);
            ReleaseAttackEvent.Invoke();
        }

        yield return new WaitForSeconds(postAttackDelay / attackSpeed);
        BreakAttack();
    }

    protected abstract void ReleaseAttack(int attackNumber);
}
