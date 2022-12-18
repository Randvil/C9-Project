using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : Entity
{
    GravityPhysics gravityPhysics;
    Movement movement;

    //jumping variables
    private float initialJumpVelocity;

    public float gravity = -9.8f;
    public float groundedGravity = -0.5f;

    [SerializeField]
    private float maxJumpHeight = 25.0f;

    [SerializeField]
    private float maxJumpTime = 1.5f;

    private void Awake()
    {
        gravityPhysics = new GravityPhysics();
        movement = new Movement();
        SetupJumpVariables();
    }

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    protected override void Update()
    {
        PlayerAttack();
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine(damage));
    }

    public void PlayerMove(Vector2 vector2)
    {
        rigidbody.velocity = movement.Move(moveSpeed, vector2, rigidbody.velocity.y);
    }

    public void PlayerJump(Vector2 vector2)
    {
        if (gravityPhysics.IsGrounded(groundLayerMask, bottomEdge, checkGroundRadius))
        {
            rigidbody.velocity = movement.Jump(initialJumpVelocity, vector2.x, moveSpeed);
        }
    }

    public void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    public void ApplyGravity(Vector2 vector2, bool isJump)
    {
        gravityPhysics.HandleGravity(rigidbody, isJump, moveSpeed, checkGroundRadius, groundLayerMask, bottomEdge, gravity, groundedGravity, vector2.x);
    }
}