using UnityEngine;

public class MovableState : TurnableState
{
    public MovableState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void Enter()
    {
        base.Enter();

        //Move();
    }

    public override void Exit()
    {
        base.Exit();

        player.Movement.BreakMove();
    }

    protected override void OnStop()
    {
        base.OnStop();

        player.Movement.BreakMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Move();
    }

    protected void Move()
    {
        if (playerInterstateData.isMoving == true && player.Movement.IsMoving == false)
        {
            player.Movement.StartMove();
        }
    }
}
