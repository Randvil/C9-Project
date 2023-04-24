using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Climb : IClimb
{
    protected MonoBehaviour owner;

    protected float speed;

    protected Rigidbody2D rigidbody;
    protected IGravity gravity;
    protected ITurning turning;

    public bool IsClimbing { get; protected set; }
    public float ClimbSpeed => rigidbody.velocity.y;
    public UnityEvent StartClimbEvent { get; } = new();
    public UnityEvent BreakClimbEvent { get;  } = new();
    public UnityEvent ClimbStateChangedEvent { get; } = new();

    public Climb(MonoBehaviour owner, ClimbData climbData, Rigidbody2D rigidbody, IGravity gravity, ITurning turning)
    {
        this.owner = owner;

        speed = climbData.speed;

        this.rigidbody = rigidbody;
        this.gravity = gravity;
        this.turning = turning;
    }

    public virtual void StartClimb()
    {
        IsClimbing = true;
        gravity.Disable(this);
        rigidbody.velocity = new(0f, 0f);

        StartClimbEvent.Invoke();
    }

    public virtual void BreakClimb()
    {
        gravity.Enable(this);
        rigidbody.velocity = new(0f, 0f);
        IsClimbing = false;

        BreakClimbEvent.Invoke();
    }

    public virtual void ClimbUp()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, speed);
        ClimbStateChangedEvent.Invoke();
    }

    public virtual void ClimbDown()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, -speed);
        ClimbStateChangedEvent.Invoke();
    }

    public virtual void StopClimb()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, 0f);
        ClimbStateChangedEvent.Invoke();
    }
}
