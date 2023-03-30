using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Jump : MonoBehaviour, IJumping
{
    private eJumpState JumpState = eJumpState.Grounded;
    public eJumpState jumpState { get => JumpState; }

    private bool isJumping;
    public bool IsJumping { get => isJumping; }

    public UnityEvent<bool> EntityJumpEvent { get; } = new();
    public UnityEvent<float> EntityJumpStateEvent { get; } = new();

    private IGravitational gravityPhysics;

    private new Rigidbody2D rigidbody;

    [SerializeField]
    private float jumpHeight = 0.5f;

    private void Start()
    {
        gravityPhysics = GetComponent<IGravitational>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateJumpState();
    }

    public void HandleJump()
    {
        if (gravityPhysics.IsGrounded() && JumpState == eJumpState.Grounded)
        {
            JumpState = eJumpState.PrepareToJump;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
        }
    }

    public void UpdateJumpState()
    {
        switch (JumpState)
        {
            case eJumpState.PrepareToJump:
                EntityJumpEvent.Invoke(true);
                JumpState = eJumpState.JumpingUp;
                isJumping = true;
                break;
            case eJumpState.JumpingUp:
                EntityJumpStateEvent.Invoke(0f);
                if (!gravityPhysics.IsGrounded() && rigidbody.velocity.y <= 0.0f)
                {   
                    JumpState = eJumpState.Falling;
                }
                break;
            case eJumpState.Falling:
                EntityJumpStateEvent.Invoke(0.5f);
                if (gravityPhysics.IsGrounded())
                {  
                    JumpState = eJumpState.Landed;
                }
                break;
            case eJumpState.Landed:
                EntityJumpStateEvent.Invoke(1f);
                JumpState = eJumpState.Grounded;
                isJumping = false;
                break;
            case eJumpState.Grounded:
                EntityJumpEvent.Invoke(false);
                break;
        }
    }

}
