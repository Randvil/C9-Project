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

        Vector2 direction = turning.Direction == eDirection.Right ? Vector2.right : Vector2.left;
        RaycastHit2D[] enemies = Physics2D.RaycastAll(caster.transform.position, direction, kanaboData.attackRadius);

        foreach (RaycastHit2D enemy in enemies)
        {
            if (enemy.collider.TryGetComponent(out ITeam enemyTeam) == false || enemyTeam.Team == team.Team)
            {
                continue;
            }
            
            if (enemy.collider.TryGetComponent(out IDamageable damageableEnemy) == true)
            {
                damageableEnemy.DamageHandler.TakeDamage(damage);
            }

            if (enemy.collider.TryGetComponent(out IEffectable effectableEnemy) == true)
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
