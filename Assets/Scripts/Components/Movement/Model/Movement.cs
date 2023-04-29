using UnityEngine;
using UnityEngine.Events;

public class Movement : IMovement
{
    private float maxSpeed;

    private Rigidbody2D rigidbody;
    private ITurning turning;
    private IEffectManager effectManager;

    private float CurrentSpeed => maxSpeed * (1f - effectManager.GetCumulativeSlowEffect());

    public bool IsMoving { get; private set; }
    public float Speed => rigidbody.velocity.x;
    public float MaxSpeed => maxSpeed;

    public UnityEvent StartMoveEvent { get; } = new();
    public UnityEvent BreakMoveEvent { get; } = new();

    public Movement(MovementData movementData, Rigidbody2D rigidbody, ITurning turning, IEffectManager effectManager)
    {
        maxSpeed = movementData.speed;

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
        StartMoveEvent.Invoke();
    }

    public void BreakMove()
    {
        rigidbody.velocity = new(0f, rigidbody.velocity.y);

        IsMoving = false;
        BreakMoveEvent.Invoke();
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
