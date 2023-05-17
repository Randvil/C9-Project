using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roll : IRoll, IProhibitable
{
    private MonoBehaviour owner;

    public float speed;
    public float duration;
    public float cooldown;
    public float colliderSizeMultiplier;
    public float damageAbsorption;

    private BoxCollider2D collider;
    private Rigidbody2D rigidbody;
    private ITurning turning;
    private IModifierManager defenceModifierManager;
    private IEffectManager effectManager;

    private Coroutine rollCoroutine;
    private float finishCooldownTime;
    private IDamageModifier damageAbsorptionModifier;

    private List<object> prohibitors = new();

    public bool IsRolling => rollCoroutine != null;
    public bool IsOnCooldown => Time.time < finishCooldownTime;
    public bool CanRoll => IsRolling == false && IsOnCooldown == false;
    public float RollDuration => duration;
    public bool IsProhibited => prohibitors.Count > 0;

    public UnityEvent StartRollEvent { get; } = new();
    public UnityEvent BreakRollEvent { get; } = new();
    public UnityEvent ProhibitionEvent { get; } = new();
    public UnityEvent AllowanceEvent { get; } = new();

    public Roll(MonoBehaviour owner, RollData rollData, BoxCollider2D collider, Rigidbody2D rigidbody, ITurning turning, IModifierManager defenceModifierManager, IEffectManager effectManager)
    {
        this.owner = owner;

        speed = rollData.speed;
        duration = rollData.duration;
        cooldown = rollData.cooldown;
        colliderSizeMultiplier = rollData.colliderSizeMultiplier;
        damageAbsorption = rollData.damageAbsorption;

        this.collider = collider;
        this.rigidbody = rigidbody;
        this.turning = turning;
        this.defenceModifierManager = defenceModifierManager;
        this.effectManager = effectManager;

        effectManager.EffectEvent.AddListener(OnRoot);
    }

    public void StartRoll()
    {
        if (IsRolling == false && CanRoll && IsProhibited == false)
        {
            rollCoroutine = owner.StartCoroutine(RollCoroutine());

            StartRollEvent.Invoke();
        }
    }

    public void BreakRoll()
    {
        if (IsRolling == false)
        {
            return;
        }

        collider.size = new(collider.size.x, collider.size.y / colliderSizeMultiplier);
        collider.offset = new(collider.offset.x, collider.offset.y + (collider.size.y * (1f - colliderSizeMultiplier) / 2f));

        rigidbody.velocity = new(0f, rigidbody.velocity.y);

        defenceModifierManager.RemoveModifier(damageAbsorptionModifier);

        owner.StopCoroutine(rollCoroutine);
        rollCoroutine = null;

        BreakRollEvent.Invoke();
    }

    private IEnumerator RollCoroutine()
    {
        finishCooldownTime = Time.time + cooldown;

        collider.offset = new(collider.offset.x, collider.offset.y - (collider.size.y * (1f - colliderSizeMultiplier) / 2f));
        collider.size = new(collider.size.x, collider.size.y * colliderSizeMultiplier);

        float directionalSpeed = turning.Direction == eDirection.Right ? speed : -speed;
        rigidbody.velocity = new(directionalSpeed, rigidbody.velocity.y);

        damageAbsorptionModifier = new RelativeDamageModifier(-damageAbsorption);
        defenceModifierManager.AddModifier(damageAbsorptionModifier);

        yield return new WaitForSeconds(duration);

        BreakRoll();
    }

    public void Prohibit(object prohibitor)
    {
        if (prohibitors.Contains(prohibitor))
        {
            return;
        }

        prohibitors.Add(prohibitor);

        if (prohibitors.Count > 1)
        {
            ProhibitionEvent.Invoke();
        }
    }

    public void Allow(object prohibitor)
    {
        if (prohibitors.Remove(prohibitor) == false)
        {
            return;
        }

        if (prohibitors.Count == 0)
        {
            AllowanceEvent.Invoke();
        }
    }

    private void OnRoot(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType != eEffectType.Root)
        {
            return;
        }

        switch (effectStatus)
        {
            case eEffectStatus.Added:
                Prohibit(effectManager);
                break;

            case eEffectStatus.Cleared:
                Allow(effectManager);
                break;
        }
    }
}
