public class StandingState : MovableState
{
    public StandingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.MovementView.SetMovementParams();
    }
}
