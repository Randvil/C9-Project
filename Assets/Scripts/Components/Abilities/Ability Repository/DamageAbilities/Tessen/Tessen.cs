using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tessen : AbstractDamageAbility, ISustainableAbility
{
    protected TessenData tessenData;

    protected BoxCollider2D collider;

    protected Damage damage;
    protected float endCastTime;

    protected Dictionary<GameObject,IDamageHandler> damagedEnemies = new();
    protected Dictionary<IEffectManager, IEffect> stunnedEnemies = new();

    public Tessen(GameObject caster, TessenData tessenData, IAbilityManager abilityManager, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team, BoxCollider2D collider) : base(caster, tessenData, abilityManager, energyManager, modifierManager, turning, team)
    {
        Type = eAbilityType.Tessen;

        this.tessenData = tessenData;

        this.collider = collider;

        damage = new Damage(caster, caster, tessenData.damageData, modifierManager);
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(tessenData.preCastDelay);

        endCastTime = Time.time + tessenData.castTime;
        finishCooldownTime = Time.time + tessenData.cooldown;

        while (Time.time < endCastTime && energyManager.Energy.currentEnergy > tessenData.cost * tessenData.impactPeriod)
        {
            Vector2 direction = turning.Direction == eDirection.Right ? Vector2.right : Vector2.left;
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(collider.transform.position, collider.size.y/2 * caster.transform.lossyScale.y, direction, tessenData.attackRadius);

            foreach (RaycastHit2D enemy in enemies)
            {
                if (enemy.collider == null)
                {
                    continue;
                }

                if (enemy.collider.TryGetComponent(out ITeam enemyTeam) == false || enemyTeam.Team == team.Team)
                {
                    continue;
                }

                if (enemy.collider.TryGetComponent(out IDamageable damageableEnemy) == true
                    && damagedEnemies.ContainsKey(enemy.collider.gameObject) == false)
                {
                    damagedEnemies.Add(enemy.collider.gameObject, damageableEnemy.DamageHandler);

                    enemy.collider.GetComponent<IGravity>().Disable(this);
                    Rigidbody2D enemyRigidbody = enemy.collider.GetComponent<Rigidbody2D>();
                    enemyRigidbody.velocity = new(enemyRigidbody.velocity.x, tessenData.ascensionalPower);
                }

                if (enemy.collider.TryGetComponent(out IEffectable stunnableEnemy) == true
                    && stunnedEnemies.ContainsKey(stunnableEnemy.EffectManager) == false)
                {
                    IEffect stunEffect = new StunEffect(float.PositiveInfinity);
                    stunnableEnemy.EffectManager.AddEffect(stunEffect);
                    stunnedEnemies.Add(stunnableEnemy.EffectManager, stunEffect);
                }
            }

            foreach (KeyValuePair<GameObject, IDamageHandler> damagedEnemy in damagedEnemies)
            {
                if (damagedEnemy.Key != null)
                {
                    damagedEnemy.Value.TakeDamage(damage, DealDamageEvent);
                }
            }

            energyManager.ChangeCurrentEnergy(-tessenData.cost * tessenData.impactPeriod);

            ReleaseCastEvent.Invoke();

            yield return new WaitForSeconds(tessenData.impactPeriod);
        }

        yield return new WaitForSeconds(tessenData.postCastDelay);

        BreakCast();
    }

    public override void BreakCast()
    {
        base.BreakCast();

        foreach (KeyValuePair<GameObject, IDamageHandler> damagedEnemy in damagedEnemies)
        {
            if (damagedEnemy.Key != null)
            {
                damagedEnemy.Key.GetComponent<IGravity>().Enable(this);
            }
        }
        damagedEnemies.Clear();

        foreach (KeyValuePair<IEffectManager, IEffect> stunnedEnemy in stunnedEnemies)
        {
            stunnedEnemy.Key.RemoveEffect(stunnedEnemy.Value);
        }
        stunnedEnemies.Clear();
    }

    public void StopSustaining()
    {
        BreakCast();
    }
}
