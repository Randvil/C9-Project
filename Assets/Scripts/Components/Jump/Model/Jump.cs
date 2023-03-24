using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IJump
{
    private JumpData jumpData;

    private Rigidbody2D rigidbody;
    private IGravity gravity;

    private float startJumpTime;
    private int airJumpNumber;

    public bool IsJumping { get; private set; }
    public bool CanJump => (gravity.IsGrounded || airJumpNumber < jumpData.maxJumpCount);

    public Jump(JumpData jumpData, Rigidbody2D rigidbody, IGravity gravity)
    {
        this.jumpData = jumpData;

        this.rigidbody = rigidbody;
        this.gravity = gravity;
    }

    public void StartJump()
    {
        gravity.Disable(this);

        if (airJumpNumber == 0 && gravity.IsGrounded == false)
        {
            airJumpNumber++;
        }

        IsJumping = true;
        startJumpTime = Time.time;
        airJumpNumber++;
    }

    public void BreakJump()
    {
        gravity.Enable(this);

        IsJumping = false;
    }

    public void UpdateJumpSpeed()
    {
        float verticalSpeed = 0f;
        if (Time.time - startJumpTime < jumpData.jumpTime)
        {
            verticalSpeed = jumpData.maxSpeed * jumpData.speedCurve.Evaluate((Time.time - startJumpTime) / jumpData.jumpTime);
        }
        rigidbody.velocity = new(rigidbody.velocity.x, verticalSpeed);
    }

    public void CheckGround()
    {
        if (gravity.IsGrounded)
        {
            airJumpNumber = 0;
        }
    }
}
