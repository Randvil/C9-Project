using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPhysics
{
    public void HandleGravity(Rigidbody rigidbody, bool isJump, float moveSpeed, float checkGroundRadius,
        LayerMask groundLayerMask, Transform bottomEdge, float gravity, float groundedGravity, float x)
    {
        bool isFalling = rigidbody.velocity.y <= 0.0f || !isJump;
        float fallMultiplier = 2.0f;

        //falling with increasing speed
        if (isFalling)
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (fallMultiplier * gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(x * moveSpeed, nextYVelocity, 0);
        }
        //jumping with decreasing speed
        else
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(x * moveSpeed, nextYVelocity, 0);
        }
    }

    public bool IsGrounded(LayerMask groundLayerMask, Transform bottomEdge, float checkGroundRadius)
    {
        return Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
    }

}
