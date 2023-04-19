using UnityEngine;

public class MovableState : TurnableState
{
    public MovableState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        Move();
    }

    public override void Exit()
    {
        base.Exit();

        player.Movement.StopMove();

        player.MovementView.SetMovementParams();
    }

    protected override void OnStop()
    {
        base.OnStop();

        player.Movement.StopMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Move();
    }

    private void Move()
    {
        if (isMoving == true)
        {
            player.Movement.StartMove();
        }

        player.MovementView.SetMovementParams();
    }
}
