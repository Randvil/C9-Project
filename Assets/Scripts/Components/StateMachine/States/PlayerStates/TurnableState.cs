
using UnityEngine;

public class TurnableState : BasePlayerState
{
    public TurnableState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void Enter()
    {
        base.Enter();

        Turn();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Turn();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Turn();
    }

    protected void Turn()
    {
        if (playerInterstateData.haveToTurn)
        {
            eDirection newDirection = player.Turning.Direction == eDirection.Left ? eDirection.Right : eDirection.Left;
            player.Turning.Turn(newDirection);
            playerInterstateData.haveToTurn = false;
        }
    }
}
