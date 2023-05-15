using UnityEngine;

public class JumpingState : MovableState
{
    public JumpingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

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

    protected override void OnAttack(eActionPhase actionPhase)
    {
        
    }
}
