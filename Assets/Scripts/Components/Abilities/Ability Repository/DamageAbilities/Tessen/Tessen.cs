using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Tessen : AbstractDamageAbility, ISustainableAbility
{
    protected float castTime;
    protected float impactPeriod;
    protected float ascensionalPower;

    protected BoxCollider2D collider;

    protected Damage damage;
    protected float endCastTime;

    protected Dictionary<GameObject,IDamageHandler> damagedEnemies = new();
    protected Dictionary<IEffectManager, IEffect> stunnedEnemies = new();

    public Tessen(MonoBehaviour owner, GameObject caster, TessenData tessenData, IEnergyManager energyManager, IModifierManager modifierManager, ITurning turning, ITeam team, BoxCollider2D collider) : base(owner, caster, tessenData, energyManager, modifierManager, turning, team)
    {
        AttackRange = tessenData.attackRange;
        castTime = tessenData.castTime;
        impactPeriod = tessenData.impactPeriod;
        ascensionalPower = tessenData.ascensionalPower;

        this.collider = collider;

        damage = new Damage(caster, caster, tessenData.damageData, modifierManager);
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(preCastDelay);

        endCastTime = Time.time + castTime;
        finishCooldownTime = Time.time + cooldown;

        while (Time.time < endCastTime && energyManager.Energy.currentEnergy > cost * impactPeriod)
        {
            Vector2 direction = turning.Direction == eDirection.Right ? Vector2.right : Vector2.left;
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(collider.transform.position, collider.size.y/2 * caster.transform.lossyScale.y, direction, AttackRange);

            foreach (RaycastHit2D enemy in enemies)
            {
                if (enemy.collider == null)
                {
                    continue;
                }

                if (enemy.collider.TryGetComponent(out ITeamMember enemyTeam) == false || enemyTeam.CharacterTeam.Team == team.Team)
                {
                    continue;
                }

                if (enemy.collider.TryGetComponent(out IEffectable stunnableEnemy) == true
                    && stunnedEnemies.ContainsKey(stunnableEnemy.EffectManager) == false)
                {
                    IEffect stunEffect = new StunEffect(float.PositiveInfinity);
                    stunnableEnemy.EffectManager.AddEffect(stunEffect);
                    stunnedEnemies.Add(stunnableEnemy.EffectManager, stunEffect);
                }

                if (enemy.collider.TryGetComponent(out IDamageable damageableEnemy) == true
                    && damagedEnemies.ContainsKey(enemy.collider.gameObject) == false)
                {
                    damagedEnemies.Add(enemy.collider.gameObject, damageableEnemy.DamageHandler);
                }

                if (enemy.collider.TryGetComponent(out IGravity enemyGravity) == true)
                {
                    enemyGravity.Disable(this);
                    Rigidbody2D enemyRigidbody = enemy.collider.GetComponent<Rigidbody2D>();
                    enemyRigidbody.velocity = new(enemyRigidbody.velocity.x, ascensionalPower);
                }
            }

            foreach (KeyValuePair<GameObject, IDamageHandler> damagedEnemy in damagedEnemies)
            {
                if (damagedEnemy.Key != null)
                {
                    damagedEnemy.Value.TakeDamage(damage, DealDamageEvent);
                }
            }

            energyManager.ChangeCurrentEnergy(-cost * impactPeriod);

            ReleaseCastEvent.Invoke();

            yield return new WaitForSeconds(impactPeriod);
        }

        yield return new WaitForSeconds(postCastDelay);

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
