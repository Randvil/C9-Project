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

    public UnityEvent<int> EntityJumpEvent { get; } = new();

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
                JumpState = eJumpState.JumpingUp;
                isJumping = true;
                break;
            case eJumpState.JumpingUp:
                EntityJumpEvent.Invoke(1);
                if (!gravityPhysics.IsGrounded() && rigidbody.velocity.y <= 0.0f)
                {   
                    JumpState = eJumpState.Falling;
                }
                break;
            case eJumpState.Falling:
                EntityJumpEvent.Invoke(2);
                if (gravityPhysics.IsGrounded())
                {  
                    JumpState = eJumpState.Landed;
                }
                break;
            case eJumpState.Landed:
                EntityJumpEvent.Invoke(3);
                StartCoroutine(GroundedCoroutine());
                isJumping = false;
                break;
            case eJumpState.Grounded:
                break;
        }
    }

    public IEnumerator GroundedCoroutine()
    {
        yield return new WaitForSeconds(.04f);
        EntityJumpEvent.Invoke(0);
        JumpState = eJumpState.Grounded;
    }
}
