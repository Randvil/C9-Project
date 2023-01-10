using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ParabolicJump : MonoBehaviour, IJump
{
    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private Transform topLeftCorner;

    [SerializeField]
    private Transform bottomRightCorner;

    //[SerializeField]
    //private float groundedGravity = -0.5f;

    [SerializeField]
    private float maxJumpHeight = 25.0f;

    [SerializeField]
    private float maxJumpTime = 1.5f;

    [SerializeField]
    private float maxFallVelocity = -5f;

    private float gravity;
    private float initialJumpVelocity;
    private bool isJumping;

    private new Rigidbody2D rigidbody;

    public bool IsJumping { get => isJumping; }

    public UnityEvent StartJumpEvent { get; } = new();

    public UnityEvent StopJumpEvent { get; } = new();

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();

        SetupJumpVariables();
    }

    private void Update()
    {
        HandleGravity();
    }

    public void StartJump()
    {
        if (IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, initialJumpVelocity);

            StartJumpEvent.Invoke();
        }
    }
    public void StopJump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);

        StopJumpEvent.Invoke();
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
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, nextYVelocity);
        }
        //jumping with decreasing speed
        else
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            if (nextYVelocity < 0f)
            {
                nextYVelocity= 0f;
                StopJumpEvent.Invoke();
            }
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, nextYVelocity);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapArea(topLeftCorner.position, bottomRightCorner.position, groundLayerMask) != null;
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
}
