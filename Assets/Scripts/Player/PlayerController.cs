using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    InputComponent inputComponent;
    MovementComponent movementComponent;
    PhysicsComponent physicsComponent;

    //jumping variables
    private float initialJumpVelocity;

    public float gravity = -9.8f;
    public float groundedGravity = -0.5f;

    [SerializeField]
    private float maxJumpHeight = 25.0f;

    [SerializeField]
    private float maxJumpTime = 1.5f;

    bool isJumpPressed;
    bool isGrounded;


    private void Awake()
    {
        physicsComponent = gameObject.AddComponent<PhysicsComponent>();
        inputComponent = gameObject.AddComponent<InputComponent>();
        movementComponent = gameObject.AddComponent<MovementComponent>();
        inputComponent.Awake();
        SetupJumpVariables();
    }

    public void OnEnable()
    {
        inputComponent.OnEnable();
    }

    public void OnDisable()
    {
        inputComponent.OnDisable();
    }

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    protected override void Update()
    {
        PlayerMove();
        physicsComponent.HandleGravity(rigidbody, isJumpPressed, moveSpeed, checkGroundRadius, groundLayerMask, bottomEdge,
          inputComponent.vectorMove.x, gravity, groundedGravity);
        PlayerJump();
        PlayerAttack();
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine(damage));
    }

    private void PlayerMove()
    {
        rigidbody.velocity = movementComponent.Move(moveSpeed, inputComponent.vectorMove, rigidbody.velocity.y);
    }
    private void PlayerJump()
    {
        isGrounded = physicsComponent.isGrounded;
        isJumpPressed = inputComponent.isJumpPressed;
        if (isJumpPressed && isGrounded)
        {
            rigidbody.velocity = movementComponent.Jump(initialJumpVelocity, inputComponent.vectorMove.x, moveSpeed);
        }
    }

    public void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
}
