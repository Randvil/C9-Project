using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tessen : AbstractDamageAbility, ISustainableAbility
{
    protected float castTime;
    protected float impactPeriod;
    protected float ascensionalPower;
    protected StunEffectData stunEffectData;

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
        stunEffectData = tessenData.stunEffectData;

        this.collider = collider;

        damage = new Damage(caster, caster, tessenData.damageData, modifierManager);
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        endCastTime = Time.time + castTime;
        finishCooldownTime = Time.time + cooldown;

        while (Time.time < endCastTime && energyManager.Energy.currentEnergy > cost * impactPeriod)
        {
            Vector2 direction = turning.Direction == eDirection.Right ? Vector2.right : Vector2.left;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(collider.transform.position, collider.size.y/2 * caster.transform.lossyScale.y, direction, AttackRange);

            foreach (RaycastHit2D hit in hits)
            {
                Collider2D enemy = hit.collider;

                if (enemy == null)
                {
                    continue;
                }

                if (team.IsSame(enemy))
                {
                    continue;
                }

                if (enemy.TryGetComponent(out IEffectable stunnableEnemy) == true
                    && stunnedEnemies.ContainsKey(stunnableEnemy.EffectManager) == false)
                {
                    IEffect stunEffect = new StunEffect(stunEffectData);
                    stunnableEnemy.EffectManager.AddEffect(stunEffect);
                    stunnedEnemies.Add(stunnableEnemy.EffectManager, stunEffect);
                }

                if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true
                    && damagedEnemies.ContainsKey(enemy.gameObject) == false)
                {
                    damagedEnemies.Add(enemy.gameObject, damageableEnemy.DamageHandler);
                }

                if (enemy.TryGetComponent(out IGravity enemyGravity) == true)
                {
                    enemyGravity.Disable(this);
                    Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                    enemyRigidbody.velocity = new(enemyRigidbody.velocity.x, ascensionalPower);
                }
            }

            foreach (KeyValuePair<GameObject, IDamageHandler> damagedEnemy in damagedEnemies)
            {
                if (damagedEnemy.Key != null)
                {
                    damagedEnemy.Value.TakeDamage(damage, this);
                }
            }

            energyManager.ChangeCurrentEnergy(-cost * impactPeriod);
            ReleaseCastEvent.Invoke();

            yield return new WaitForSeconds(impactPeriod);
        }
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
