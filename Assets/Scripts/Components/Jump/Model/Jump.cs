using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jump : IJump
{
    private MonoBehaviour owner;

    private float jumpTime;
    private int maxJumpCount;
    private float maxSpeed;
    private AnimationCurve speedCurve;

    private Rigidbody2D rigidbody;
    private IGravity gravity;

    private float startJumpTime;
    private int airJumpNumber;
    private Coroutine jumpSpeedCoroutine;

    public bool IsJumping { get; private set; }
    public bool CanJump => (gravity.IsGrounded || airJumpNumber < maxJumpCount);
    public UnityEvent StartJumpEvent { get; } = new();
    public UnityEvent BreakJumpEvent { get; } = new();

    public Jump(MonoBehaviour owner, JumpData jumpData, Rigidbody2D rigidbody, IGravity gravity)
    {
        this.owner = owner;

        maxJumpCount= jumpData.maxJumpCount;
        jumpTime = jumpData.jumpTime;
        maxSpeed = jumpData.maxSpeed;
        speedCurve = jumpData.speedCurve;

        this.rigidbody = rigidbody;
        this.gravity = gravity;

        gravity.GroundedEvent.AddListener(OnGrounded);
    }

    public void StartJump()
    {
        if (jumpSpeedCoroutine != null)
        {
            owner.StopCoroutine(jumpSpeedCoroutine);
        }

        gravity.Disable(this);

        if (airJumpNumber == 0 && gravity.IsGrounded == false)
        {
            airJumpNumber++;
        }

        startJumpTime = Time.time;
        airJumpNumber++;

        jumpSpeedCoroutine = owner.StartCoroutine(JumpSpeedCoroutine());

        IsJumping = true;
        StartJumpEvent.Invoke();
    }

    public void BreakJump()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, 0f);
        IsJumping = false;

        gravity.Enable(this);

        BreakJumpEvent.Invoke();
    }

    private IEnumerator JumpSpeedCoroutine()
    {
        while (Time.time - startJumpTime < jumpTime)
        {
            float verticalSpeed = maxSpeed * speedCurve.Evaluate((Time.time - startJumpTime) / jumpTime);
            rigidbody.velocity = new(rigidbody.velocity.x, verticalSpeed);

            yield return new WaitForFixedUpdate();
        }

        BreakJump();
    }

    private void OnGrounded()
    {
        airJumpNumber = 0;
    }
}
