using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefensiveJump : AbstractAbility
{
    protected float jumpTime;
    protected Vector2 initialSpeed;
    protected AnimationCurve speedCurve;

    protected Rigidbody2D rigidbody;
    protected IGravity gravity;
    protected ITurning turning;

    protected float startJumpTime;

    public DefensiveJump(MonoBehaviour owner, DefensiveJumpData defensiveJumpData, IEnergyManager energyManager, Rigidbody2D rigidbody, IGravity gravity, ITurning turning) : base(owner, defensiveJumpData, energyManager)
    {
        jumpTime = defensiveJumpData.jumpTime;
        initialSpeed = defensiveJumpData.initialJumpSpeed;
        speedCurve = defensiveJumpData.speedCurve;

        this.rigidbody = rigidbody;
        this.gravity = gravity;
        this.turning = turning;
    }

    public override void StartCast()
    {
        if (IsPerforming == false)
        {
            strikeCoroutine = owner.StartCoroutine(ReleaseStrikeCoroutine());

            StartCastEvent.Invoke();
        }
    }

    public override void BreakCast()
    {
        if (IsPerforming == true)
        {
            owner.StopCoroutine(strikeCoroutine);
            strikeCoroutine = null;
            rigidbody.velocity = new(0f, 0f);
            gravity.Enable(this);

            BreakCastEvent.Invoke();
        }
    }

    protected override IEnumerator ReleaseStrikeCoroutine()
    {
        yield return new WaitForSeconds(preCastDelay);

        finishCooldownTime = Time.time + cooldown;
        energyManager.ChangeCurrentEnergy(-cost);

        gravity.Disable(this);
        startJumpTime = Time.time;
        float horizontalSpeed = turning.Direction == eDirection.Right ? initialSpeed.x : -initialSpeed.x;
        rigidbody.velocity = new(horizontalSpeed, 0f);
        while (Time.time - startJumpTime < jumpTime)
        {
            float verticalSpeed = initialSpeed.y * speedCurve.Evaluate((Time.time - startJumpTime) / jumpTime);
            rigidbody.velocity = new(rigidbody.velocity.x, verticalSpeed);

            yield return new WaitForFixedUpdate();
        }

        gravity.Enable(this);

        yield return new WaitUntil(() => gravity.IsGrounded == true);
        rigidbody.velocity = new(0f, 0f);

        yield return new WaitForSeconds(postCastDelay);

        BreakCast();
    }
}
