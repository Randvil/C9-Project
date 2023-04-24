using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravity : MonoBehaviour, IGravity
{
    [SerializeField]
    private Collider2D checkGroundCollider;

    [SerializeField]
    private GravityData gravityData;

    private LayerMask groundLayer;
    private AnimationCurve fallSpeedCurve;
    private float maxFallSpeed;
    private float maxSpeedTime;

    private float startFallTime;
    private bool wasFalling;
    private bool wasGrounded;
    private readonly List<object> disablers = new();

    private new Rigidbody2D rigidbody;

    public bool IsDisabled => disablers.Count > 0;
    public bool IsGrounded { get; private set; }
    public bool IsFalling { get; private set; }

    public UnityEvent StartFallEvent { get; private set; } = new();
    public UnityEvent BreakFallEvent { get; private set; } = new();
    public UnityEvent GroundedEvent { get; private set; } = new();

    private void Awake()
    {
        groundLayer = gravityData.groundLayer;
        fallSpeedCurve = gravityData.fallSpeedCurve;
        maxFallSpeed = gravityData.maxFallSpeed;
        maxSpeedTime = gravityData.maxSpeedTime;

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetFallingState();
        HandleFallSpeed();
    }

    public void SetFallingState()
    {
        if (IsDisabled == true)
        {
            if (wasFalling == true)
            {
                wasFalling = false;
                BreakFallEvent.Invoke();
            }

            IsFalling = false;
            return;
        }

        IsGrounded = checkGroundCollider.IsTouchingLayers(groundLayer);
        if (IsGrounded == true)
        {
            if (wasGrounded == false)
            {
                //rigidbody.velocity = new(rigidbody.velocity.x, 0f);

                wasGrounded = true;
                GroundedEvent.Invoke();
                BreakFallEvent.Invoke();
            }

            IsFalling = false;
            return;
        }

        if (IsFalling == false)
        {
            startFallTime = Time.time;
            StartFallEvent.Invoke();
        }

        IsFalling = wasFalling = true;
        wasGrounded = false;
    }

    private void HandleFallSpeed()
    {
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

        if (IsDisabled == false)
        {
            SetFallingState();
        }
    }
}
