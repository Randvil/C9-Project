using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crouch : ICrouch
{
    private float colliderSizeMultiplier;

    private BoxCollider2D collider;
    private IEffectManager effectManager;
    private ISlowEffect slowEffect;

    public float CrouchSpeed { get; private set; }
    public UnityEvent StartCrouchEvent { get; private set; } = new();
    public UnityEvent BreakCrouchEvent { get; private set; } = new();


    public Crouch(CrouchData crouchData, BoxCollider2D collider, IEffectManager effectManager)
    {
        colliderSizeMultiplier = crouchData.colliderSizeMultiplier;

        this.collider = collider;
        this.effectManager = effectManager;

        slowEffect = new SlowEffect(crouchData.slowEffectData);
    }

    public void StartCrouch()
    {
        collider.offset = new(collider.offset.x, collider.offset.y - (collider.size.y * (1f - colliderSizeMultiplier) / 2f));
        collider.size = new(collider.size.x, collider.size.y * colliderSizeMultiplier);

        effectManager.AddEffect(slowEffect);

        StartCrouchEvent.Invoke();
    }

    public void BreakCrouch()
    {
        collider.size = new(collider.size.x, collider.size.y / colliderSizeMultiplier);
        collider.offset = new(collider.offset.x, collider.offset.y + (collider.size.y * (1f - colliderSizeMultiplier) / 2f));

        effectManager.RemoveEffect(slowEffect);

        BreakCrouchEvent.Invoke();
    }
}
