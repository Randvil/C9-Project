using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : IRoll
{
    private RollData rollData;

    private BoxCollider2D collider;
    private Rigidbody2D rigidbody;
    private ITurning turning;
    private IModifierManager defenceModifierManager;

    private Coroutine rollCoroutine;
    private float finishCooldownTime;
    private IDamageModifier damageAbsorption;

    public bool IsRolling => rollCoroutine != null;
    public bool IsOnCooldown => Time.time < finishCooldownTime;
    public bool CanRoll => IsRolling == false && IsOnCooldown == false;

    public Roll(RollData rollData, BoxCollider2D collider, Rigidbody2D rigidbody, ITurning turning, IModifierManager defenceModifierManager)
    {
        this.rollData = rollData;

        this.collider = collider;
        this.rigidbody = rigidbody;
        this.turning = turning;
        this.defenceModifierManager = defenceModifierManager;
    }

    public void StartRoll()
    {
        rollCoroutine = Coroutines.StartCoroutine(RollCoroutine());
    }

    public void BreakRoll()
    {
        if (IsRolling == false)
        {
            return;
        }

        collider.size = new(collider.size.x, collider.size.y / rollData.colliderSizeMultiplier);
        collider.offset = new(collider.offset.x, collider.offset.y + (collider.size.y * (1f - rollData.colliderSizeMultiplier) / 2f));

        rigidbody.velocity = new(0f, rigidbody.velocity.y);

        defenceModifierManager.RemoveModifier(damageAbsorption);

        Coroutines.StopCoroutine(ref rollCoroutine);
    }

    private IEnumerator RollCoroutine()
    {
        finishCooldownTime = Time.time + rollData.cooldown;

        collider.offset = new(collider.offset.x, collider.offset.y - (collider.size.y * (1f - rollData.colliderSizeMultiplier) / 2f));
        collider.size = new(collider.size.x, collider.size.y * rollData.colliderSizeMultiplier);

        float directionalSpeed = turning.Direction == eDirection.Right ? rollData.speed : -rollData.speed;
        rigidbody.velocity = new(directionalSpeed, rigidbody.velocity.y);

        damageAbsorption = new RelativeDamageModifier(-rollData.damageAbsorption);
        defenceModifierManager.AddModifier(damageAbsorption);

        yield return new WaitForSeconds(rollData.duration);

        BreakRoll();
    }
}
