using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerController : Entity
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
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

        // jumping
        isJumping = !Physics.CheckSphere(bottomEdge.position, checkGroundRadius, groundLayerMask);
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) rigidbody.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.VelocityChange);

        // attack
        if (Input.GetMouseButtonDown(0) && attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine());
    }
}
