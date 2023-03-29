public class TurnableState : BasePlayerState
{
    public TurnableState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void LogicUpdate()
    {
        if (haveToTurn)
        {
            eDirection newDirection = player.Turning.Direction == eDirection.Left ? eDirection.Right : eDirection.Left;
            player.Turning.Turn(newDirection);
            haveToTurn = false;
        }

        base.LogicUpdate();
    }
}
