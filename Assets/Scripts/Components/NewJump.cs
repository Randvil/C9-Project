using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private Transform bottomEdge;

    [SerializeField]
    private float checkGroundRadius;

    [SerializeField]
    private float gravity = -9.8f;

    [SerializeField]
    private float groundedGravity = -0.5f;

    [SerializeField]
    private float maxJumpHeight = 25.0f;

    [SerializeField]
    private float maxJumpTime = 1.5f;

    [SerializeField]
    private float maxFallVelocity = -5f;

    private float initialJumpVelocity;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody>();

        SetupJumpVariables();
    }

    private void Update()
    {
        HandleGravity();
    }

    public void Jump()
    {
        if (IsGrounded(groundLayerMask, bottomEdge, checkGroundRadius))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, initialJumpVelocity, rigidbody.velocity.z);
        }
    }


    private void HandleGravity()
    {
        bool isFalling = rigidbody.velocity.y <= 0.0f;
        float fallMultiplier = 2.0f;

        //falling with increasing speed
        if (isFalling)
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (fallMultiplier * gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            nextYVelocity = nextYVelocity > maxFallVelocity ? maxFallVelocity : nextYVelocity;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, nextYVelocity, rigidbody.velocity.z);
        }
        //jumping with decreasing speed
        else
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, nextYVelocity, rigidbody.velocity.z);
        }
    }

    private bool IsGrounded(LayerMask groundLayerMask, Transform bottomEdge, float checkGroundRadius)
    {
        return Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
}
