using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    private PlayerInput playerInput;

    //gravity variables
    private float gravity = -9.8f;
    private float groundedGravity = -0.5f;

    //jumping variables
    private bool isJumpPressed;
    private float initialJumpVelocity;
    [SerializeField]
    private float maxJumpHeight = 25.0f;
    [SerializeField]
    private float maxJumpTime = 1.5f;
    private bool isGrounded;

    private void Awake()
    {
        playerInput = new PlayerInput();

        //input callbacks for jumping
        playerInput.PlayerControls.Jump.started += onJump;
        playerInput.PlayerControls.Jump.canceled += onJump;

        SetupJumpVariables();
    }

    private void OnEnable()
    {
        playerInput.PlayerControls.Enable();
    }

    protected override void Start()
    {
        base.Start();

        rigidbody = GetComponentInParent<Rigidbody>();
    }

    protected override void Update()
    {
        PlayerMovement();
        HandleGravity();
        PlayerAttack();
        PlayerJump();
    }
    
    private void PlayerMovement()
    {
        // moving
        float deltaX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        rigidbody.velocity = new Vector3(deltaX, rigidbody.velocity.y, 0f);
        
        // turning
        if (deltaX > 0f && direction != 0f)
        {
            direction = 0f;
            if (turnCoroutine != null) StopCoroutine(turnCoroutine);
            turnCoroutine = StartCoroutine(TurnCoroutine(direction));
        }
        else if (deltaX < 0f && direction != 180f)
        {
            direction = 180;
            if (turnCoroutine != null) StopCoroutine(turnCoroutine);
            turnCoroutine = StartCoroutine(TurnCoroutine(direction));
        }
    }

    //attack
    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine(damage));
    }

    //changes isJumpPressed when Space button is pressed
    private void onJump (InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void HandleGravity()
    {
        isGrounded = Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);

        bool isFalling = rigidbody.velocity.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;

        if (isGrounded)
        {
            rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed,  groundedGravity, 0);
        } else if (isFalling)
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (fallMultiplier * gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, nextYVelocity, 0);
        } else
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, nextYVelocity, 0);
        }
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void PlayerJump()
    {
        // jumping
        isGrounded = Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
        if (isJumpPressed && isGrounded)
        {
            rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, initialJumpVelocity * .5f, 0);
        }
    }
}
