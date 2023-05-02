using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kanabo : AbstractDamageAbility, IAbility
{
    protected StunEffectData stunEffectData;

    protected Damage damage;

    public Kanabo(MonoBehaviour owner, GameObject caster, KanaboData kanaboData, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team) : base(owner, caster, kanaboData, energyManager, modifierManager, turning, team)
    {
        Type = eAbilityType.Kanabo;
        
        AttackRange = kanaboData.attackRange;
        stunEffectData = kanaboData.stunEffectData;

        damage = new(caster, caster, damageData, modifierManager);
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(preCastDelay);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(caster.transform.position, AttackRange);

        foreach (Collider2D enemy in enemies)
        {
            if (team.IsSame(enemy))
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
                damageableEnemy.DamageHandler.TakeDamage(damage, this);
            }

            if (enemy.TryGetComponent(out IEffectable effectableEnemy) == true)
            {
                effectableEnemy.EffectManager.AddEffect(new StunEffect(stunEffectData));
            }
        }

        energyManager.ChangeCurrentEnergy(-cost);

        finishCooldownTime = Time.time + cooldown;
        ReleaseCastEvent.Invoke();

        yield return new WaitForSeconds(postCastDelay);

        BreakCast();
    }
}
