using UnityEngine;

public class JumpingState : MovableState
{
    public JumpingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.Jump.StartJump();
        player.JumpView.StartJump();

        player.GravityView.SetFallingParams();
    }

    public override void Exit()
    {
        base.Exit();

        player.Jump.BreakJump();
        player.JumpView.BreakJump();

        player.GravityView.SetFallingParams();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.Jump.UpdateJumpSpeed();

        if (player.Rigidbody.velocity.y <= 0f)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }
}
