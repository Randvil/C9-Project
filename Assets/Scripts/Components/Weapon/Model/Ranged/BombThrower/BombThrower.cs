using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : AbstractRangedWeapon
{
    private GameObject currentBombObject;
    private IProjectile bomb;

    public BombThrower(MonoBehaviour owner, GameObject weaponOwner, Transform projectileSpawnPoint, RangedWeaponData bombThrowerData, IModifierManager weaponModifierManager, ITeam team, ITurning turning) : base(owner, weaponOwner, weaponModifierManager, team, turning, projectileSpawnPoint, bombThrowerData) { }

    protected override IEnumerator AttackCoroutine()
    {
        for (int attackNumber = 1; attackNumber <= Mathf.Min(attackSeries, preAttackDelays.Length); attackNumber++)
        {
            currentBombObject = Object.Instantiate(projectilePrefab, new(projectileSpawnPoint.position.x, projectileSpawnPoint.position.y, 0f), Quaternion.identity);
            bomb = currentBombObject.GetComponent<IProjectile>();

            yield return new WaitForSeconds(preAttackDelays[attackNumber - 1] / attackSpeed);
            ReleaseAttack(attackNumber);
            ReleaseAttackEvent.Invoke();

            yield return new WaitForSeconds(postAttackDelay / attackSpeed);
        }
        
        BreakAttack();
    }

    protected override void ReleaseAttack(int attackNumber)
    {
        DamageData damageData = damageDatas.Length >= attackNumber ? damageDatas[attackNumber - 1] : damageDatas[0];

        bomb.Release(weaponOwnerObject, damageData, turning.Direction, team, weaponModifierManager, this);
        currentBombObject = null;
    }

    public override void BreakAttack()
    {
        if (attackCoroutine != null)
        {
            owner.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            attackSeries = 0;

            if (currentBombObject != null)
            {
                Object.Destroy(currentBombObject);
            }

            BreakAttackEvent.Invoke();
        }
    }
}
