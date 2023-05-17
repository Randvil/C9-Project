using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jump : IJump, IProhibitable
{
    private MonoBehaviour owner;

    private float jumpTime;
    private int maxJumpCount;
    private float maxSpeed;
    private AnimationCurve speedCurve;

    private Rigidbody2D rigidbody;
    private IGravity gravity;
    private IEffectManager effectManager;

    private float startJumpTime;
    private int airJumpNumber;
    private Coroutine jumpSpeedCoroutine;

    private List<object> prohibitors = new();

    public bool IsJumping { get; private set; }
    public bool CanJump => (gravity.IsGrounded || airJumpNumber < maxJumpCount);
    public bool IsProhibited => prohibitors.Count > 0;

    public UnityEvent StartJumpEvent { get; } = new();
    public UnityEvent BreakJumpEvent { get; } = new();
    public UnityEvent ProhibitionEvent { get; } = new();
    public UnityEvent AllowanceEvent { get; } = new();

    public Jump(MonoBehaviour owner, JumpData jumpData, Rigidbody2D rigidbody, IGravity gravity, IEffectManager effectManager)
    {
        this.owner = owner;

        maxJumpCount = jumpData.maxJumpCount;
        jumpTime = jumpData.jumpTime;
        maxSpeed = jumpData.maxSpeed;
        speedCurve = jumpData.speedCurve;

        this.rigidbody = rigidbody;
        this.gravity = gravity;
        this.effectManager = effectManager;

        gravity.GroundedEvent.AddListener(OnGrounded);
        effectManager.EffectEvent.AddListener(OnRoot);
    }

    public void StartJump()
    {
        if (CanJump == false || IsProhibited)
        {
            return;
        }

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
