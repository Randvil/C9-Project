using UnityEngine;

public class Movement : IMovement
{
    private MovementData movementData;

    private Rigidbody2D rigidbody;
    private ITurning turning;
    private IEffectManager effectManager;

    private float CurrentSpeed => movementData.speed * (1f - effectManager.GetCumulativeSlowEffect());

    public float Speed => rigidbody.velocity.x;
    public bool IsMoving { get; private set; }

    public Movement(MovementData movementData, Rigidbody2D rigidbody, ITurning turning, IEffectManager effectManager)
    {
        this.movementData = movementData;
        this.rigidbody = rigidbody;
        this.turning = turning;
        this.effectManager = effectManager;

        effectManager.EffectEvent.AddListener(OnSlow);
    }

    public void StartMove()
    {
        float directionalSpeed = turning.Direction == eDirection.Right ? CurrentSpeed : -CurrentSpeed;
        rigidbody.velocity = new(directionalSpeed, rigidbody.velocity.y);

        IsMoving = true;
    }

    public void StopMove()
    {
        rigidbody.velocity = new(0f, rigidbody.velocity.y);

        IsMoving = false;
    }

    private void OnSlow(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType != eEffectType.Slow)
        {
            return;
        }

        if (IsMoving)
        {
            StartMove();
        }
    }
}
