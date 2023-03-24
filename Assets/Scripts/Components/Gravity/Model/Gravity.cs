using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Gravity : MonoBehaviour, IGravity
{
    [SerializeField]
    private Collider2D checkGroundCollider;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private AnimationCurve fallSpeedCurve;

    [SerializeField, Min(0f)]
    private float maxFallSpeed = 2f;

    [SerializeField, Min(0f)]
    private float maxSpeedTime = 1f;

    private readonly List<object> disablers = new();
    private new Rigidbody2D rigidbody;
    private float startFallTime;

    public bool IsDisabled => disablers.Count > 0;
    public bool IsGrounded { get; private set; }
    public bool IsFalling { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        IsGrounded = checkGroundCollider.IsTouchingLayers(groundLayer);
        SetFallingState();
        HandleGravity();
    }

    private void SetFallingState()
    {
        if (IsDisabled == true)
        {
            IsFalling = false;
            return;
        }

        if (IsGrounded == true)
        {
            IsFalling = false;
            return;
        }

        if (IsFalling == false)
        {
            startFallTime = Time.time;
        }

        IsFalling = true;
    }

    private void HandleGravity()
    {
        //if (IsFalling == false)
        //{
        //    return;
        //}

        if (IsDisabled == true || IsFalling == false)
        {
            return;
        }

        float verticalSpeed = Time.time - startFallTime > maxSpeedTime
            ? -maxFallSpeed
            : -maxFallSpeed * fallSpeedCurve.Evaluate((Time.time - startFallTime) / maxSpeedTime);

        rigidbody.velocity = new(rigidbody.velocity.x, verticalSpeed);
    }

    public void Disable(object disabler)
    {
        if (disablers.Contains(disabler) == false)
        {
            disablers.Add(disabler);
        }
    }

    public void Enable(object disabler)
    {
        disablers.Remove(disabler);
    }
}
