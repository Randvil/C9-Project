using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
    public bool isGrounded;

    public void HandleGravity(Rigidbody rigidbody, bool isJump, float moveSpeed, float checkGroundRadius,
        LayerMask groundLayerMask, Transform bottomEdge, float x, float gravity, float groundedGravity)
    {
        isGrounded = Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);

        bool isFalling = rigidbody.velocity.y <= 0.0f || !isJump;
        float fallMultiplier = 2.0f;

        if (isGrounded)
        {
            rigidbody.velocity = new Vector3(x * moveSpeed, groundedGravity, 0);
        }
        else if (isFalling)
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (fallMultiplier * gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(x * moveSpeed, nextYVelocity, 0);
        }
        else
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(x * moveSpeed, nextYVelocity, 0);
        }
    }

}
