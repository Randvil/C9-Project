using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class Gravity : MonoBehaviour, IGravitational
{
    [SerializeField]
    private Transform topLeftCorner;

    [SerializeField]
    private Transform bottomRightCorner;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private float gravity = -9.8f;

    [SerializeField]
    private float fallMultiplierStart = 2.0f;

    [SerializeField]
    private float maxFallVelocity = -5f;

    public UnityEvent<int> GravityFallEvent { get; } = new();

    private eJumpState fallState = eJumpState.Grounded;
    public eJumpState FallState { get => fallState; }

    private new Rigidbody2D rigidbody;
    private IJumping jumping;
    private IClimb climb;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        jumping = GetComponent<IJumping>();
        climb = GetComponent<IClimb>();
    }

    private void Update()
    {
        HandleFallGravity();   
        HandleJumpGravity();
        HandleClimbGravity();
    }

    public void HandleJumpGravity()
    {
        float fallMultiplier = fallMultiplierStart; 

        //falling with increasing speed
        if (jumping.jumpState == eJumpState.Falling)
        {
            GravityWhileFalling(fallMultiplier);
        }
        else
        //jumping with decreasing speed
        if (jumping.jumpState == eJumpState.JumpingUp)
        {
            float previousYVelocity = rigidbody.velocity.y;
            float newYVelocity = rigidbody.velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            if (nextYVelocity < 0f)
            {
                nextYVelocity = 0f;
            }
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, nextYVelocity);
        } else return;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapArea(topLeftCorner.position, bottomRightCorner.position, groundLayerMask) != null;
    }

    public void HandleClimbGravity()
    {
        float fallMultiplier = fallMultiplierStart;

        switch (climb.ClimbState)
        {
            case eClimbState.ClimbingUp:
                DisableGravity();
                break;
            case eClimbState.ClimbingDown:
                DisableGravity();
                break;
            case eClimbState.Hanging:
                DisableGravity();
                break;
            case eClimbState.Grounded:
                EnableGravity();
                break;
            case eClimbState.JumpingOff:
                EnableGravity();
                fallState = eJumpState.Falling;
                break;
        }       
    }
    public void GravityWhileFalling(float fallMultiplier)
    {
        float previousYVelocity = rigidbody.velocity.y;
        float newYVelocity = rigidbody.velocity.y + (fallMultiplier * gravity * Time.deltaTime);
        float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
        nextYVelocity = nextYVelocity > maxFallVelocity ? maxFallVelocity : nextYVelocity;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, nextYVelocity);
    }

    public void HandleFallGravity()
    {
        if ((!IsGrounded() && jumping.jumpState == eJumpState.Grounded && climb.ClimbState == eClimbState.Grounded) || climb.ClimbState == eClimbState.JumpingOff)
        {
            fallState = eJumpState.Falling;
            GravityWhileFalling(fallMultiplierStart);
        }
        switch (fallState)
        {
            case eJumpState.Falling:
                GravityFallEvent.Invoke(2);
                if (IsGrounded())
                {
                    fallState = eJumpState.Landed;
                }
                break;
            case eJumpState.Landed:
                GravityFallEvent.Invoke(3);
                StartCoroutine(GroundedCoroutine());
                break;
            case eJumpState.Grounded:
                GravityFallEvent.Invoke(0);
                break;
        }
    }

    public void DisableGravity()
    {
        rigidbody.gravityScale = 0;
    }

    public void EnableGravity()
    {
        rigidbody.gravityScale = 1;
    }
    public IEnumerator GroundedCoroutine()
    {
        yield return new WaitForSeconds(.04f);
        fallState = eJumpState.Grounded;
    }
}