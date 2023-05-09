using UnityEngine;

public class RollingState : BasePlayerState
{
    public RollingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void Enter()
    {
        base.Enter();
        player.Roll.BreakRollEvent.AddListener(OnBreakRoll);

        player.Roll.StartRoll();
    }

    public override void Exit()
    {
        base.Exit();
        player.Roll.BreakRollEvent.RemoveListener(OnBreakRoll);

        player.Roll.BreakRoll();
    }

    protected override void OnRoll(eActionPhase actionPhase)
    {

    }

    private void OnBreakRoll()
    {
        stateMachine.ChangeState(player.Standing);
    }
}
