using UnityEngine;

public class JumpingState : MovableState
{
    public JumpingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.Jump.StartJump();

        player.Gravity.SetFallingState();
    }

    public override void Exit()
    {
        base.Exit();

        player.Jump.BreakJump();

        player.Gravity.SetFallingState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (player.Rigidbody.velocity.y <= 0f)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }
}
