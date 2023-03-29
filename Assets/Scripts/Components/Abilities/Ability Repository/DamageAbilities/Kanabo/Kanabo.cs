using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kanabo : AbstractDamageAbility, IAbility
{
    protected KanaboData kanaboData;

    protected Damage damage;

    public Kanabo(GameObject caster, KanaboData kanaboData, IAbilityManager abilityManager, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team) : base(caster, kanaboData, abilityManager, energyManager, modifierManager, turning, team)
    {
        this.kanaboData = kanaboData;

        damage = new(caster, caster, damageAbilityData.damageData, modifierManager);
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(kanaboData.preCastDelay);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(caster.transform.position, kanaboData.attackRadius);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out ITeam enemyTeam) == false || enemyTeam.Team == team.Team)
            {
                continue;
            }

            float enemyRealtivePosition = enemy.transform.position.x - caster.transform.position.x;
            if ((turning.Direction == eDirection.Right && enemyRealtivePosition < 0f)
                || (turning.Direction == eDirection.Left && enemyRealtivePosition > 0f))
            {
                continue;
            }

            if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                damageableEnemy.DamageHandler.TakeDamage(damage);
            }

            if (enemy.TryGetComponent(out IEffectable effectableEnemy) == true)
            {
                effectableEnemy.EffectManager.AddEffect(new StunEffect(Time.time + kanaboData.stunDuration));
            }
        }

        energyManager.ChangeCurrentEnergy(-kanaboData.cost);

        finishCooldownTime = Time.time + kanaboData.cooldown;
        ReleaseCastEvent.Invoke();

        yield return new WaitForSeconds(kanaboData.postCastDelay);

        BreakCast();
    }
}
