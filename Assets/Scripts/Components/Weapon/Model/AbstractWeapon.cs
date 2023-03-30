using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractWeapon : IWeapon
{
    protected GameObject weaponOwner;

    protected WeaponData weaponData;

    protected IModifierManager weaponModifierManager;
    protected ITeam team;
    protected ITurning turning;

    protected Coroutine attackCoroutine;
    protected int attackSeries;

    public float AttackSpeed => weaponData.attackSpeed;
    public bool IsAttacking => attackCoroutine != null;

    public UnityEvent ReleaseAttackEvent { get; } = new();

    public AbstractWeapon(GameObject weaponOwner, WeaponData weaponData, IModifierManager weaponModifierManager, ITeam team, ITurning turning)
    {
        this.weaponOwner = weaponOwner;

        this.weaponData = weaponData;

        this.weaponModifierManager = weaponModifierManager;
        this.team = team;
        this.turning = turning;
    }

    public virtual void StartAttack()
    {
        attackSeries++;

        if (attackCoroutine == null)
        {
            attackCoroutine = Coroutines.StartCoroutine(AttackCoroutine());
        }
    }

    public virtual void BreakAttack()
    {
        if (attackCoroutine != null)
        {
            Coroutines.StopCoroutine(ref attackCoroutine);
            attackSeries = 0;
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        for (int attackNumber = 1; attackNumber <= Mathf.Min(attackSeries, weaponData.preAttackDelays.Length); attackNumber++)
        {
            yield return new WaitForSeconds(weaponData.preAttackDelays[attackNumber - 1] / weaponData.attackSpeed);
            ReleaseAttack();
            ReleaseAttackEvent.Invoke();
        }

        yield return new WaitForSeconds(weaponData.postAttackDelay / weaponData.attackSpeed);
        BreakAttack();
    }

    protected abstract void ReleaseAttack();
}
