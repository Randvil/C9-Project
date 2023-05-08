public class TurnableState : BasePlayerState
{
    public TurnableState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void LogicUpdate()
    {
        if (playerInterstateData.haveToTurn)
        {
            eDirection newDirection = player.Turning.Direction == eDirection.Left ? eDirection.Right : eDirection.Left;
            player.Turning.Turn(newDirection);
            playerInterstateData.haveToTurn = false;
        }

        base.LogicUpdate();
    }
}
