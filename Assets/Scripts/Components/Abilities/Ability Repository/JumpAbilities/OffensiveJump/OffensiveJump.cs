using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OffensiveJump : DefensiveJump, IDamageDealer
{
    protected BoxCollider2D collider;
    protected ITeam characterTeam;

    protected Damage damage;
    protected Coroutine damageCoroutine;
    protected LayerMask enemyLayers;
    protected List<Collider2D> hitEnemies = new();

    public UnityEvent<DamageInfo> DealDamageEventCallback { get; } = new();

    public OffensiveJump(MonoBehaviour owner, OffensiveJumpData offensiveJumpData, IEnergyManager energyManager, Rigidbody2D rigidbody, BoxCollider2D collider, IGravity gravity, ITurning turning, ITeam characterTeam, IModifierManager modifierManager) : base(owner, offensiveJumpData, energyManager, rigidbody, gravity, turning)
    {
        this.collider = collider;
        this.characterTeam = characterTeam;

        enemyLayers = offensiveJumpData.enemyLayers;
        damage = new Damage(owner.gameObject, owner.gameObject, offensiveJumpData.damageData, modifierManager);
    }

    public override void StartCast()
    {
        if (IsPerforming == false)
        {
            abilityCoroutine = owner.StartCoroutine(AbilityCoroutine());

            damageCoroutine = owner.StartCoroutine(DamageCoroutine());

            StartCastEvent.Invoke();
        }
    }

    public override void BreakCast()
    {
        if (IsPerforming == true)
        {
            owner.StopCoroutine(abilityCoroutine);
            abilityCoroutine = null;
            rigidbody.velocity = new(0f, 0f);
            gravity.Enable(this);

            owner.StopCoroutine(damageCoroutine);
            damageCoroutine = null;
            hitEnemies.Clear();

            BreakCastEvent.Invoke();
        }
    }

    protected IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(preCastDelay);

        while (IsPerforming)
        {
            Collider2D[] enemies = Physics2D.OverlapBoxAll(collider.transform.position, collider.size, 0f, enemyLayers);

            foreach (Collider2D enemy in enemies)
            {
                if (hitEnemies.Contains(enemy) == false)
                {
                    hitEnemies.Add(enemy);

                    if (characterTeam.IsSame(enemy))
                    {
                        break;
                    }

                    if (enemy.TryGetComponent(out IDamageable damageableEnemy) == true)
                    {
                        damageableEnemy.DamageHandler.TakeDamage(damage, this);
                    }
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
