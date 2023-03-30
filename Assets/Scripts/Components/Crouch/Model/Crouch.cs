using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : ICrouch
{
    private CrouchData crouchData;

    private BoxCollider2D collider;
    private IEffectManager effectManager;
    private ISlowEffect slowEffect;

    public Crouch(CrouchData crouchData, BoxCollider2D collider, IEffectManager effectManager)
    {
        this.crouchData = crouchData;

        this.collider = collider;
        this.effectManager = effectManager;

        slowEffect = new SlowEffect(crouchData.movementSlow, float.MaxValue);
    }

    public void StartCrouch()
    {
        collider.offset = new(collider.offset.x, collider.offset.y - (collider.size.y * (1f - crouchData.colliderSizeMultiplier) / 2f));
        collider.size = new(collider.size.x, collider.size.y * crouchData.colliderSizeMultiplier);

        effectManager.AddEffect(slowEffect);
    }

    public void BreakCrouch()
    {
        collider.size = new(collider.size.x, collider.size.y / crouchData.colliderSizeMultiplier);
        collider.offset = new(collider.offset.x, collider.offset.y + (collider.size.y * (1f - crouchData.colliderSizeMultiplier) / 2f));

        effectManager.RemoveEffect(slowEffect);
    }
}
