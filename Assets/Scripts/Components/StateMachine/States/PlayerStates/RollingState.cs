using UnityEngine;

public class RollingState : BasePlayerState
{
    public RollingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.Roll.StartRoll();
        player.RollView.StartRoll();
    }

    public override void Exit()
    {
        base.Exit();

        player.Roll.BreakRoll();
        player.RollView.BreakRoll();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.Roll.IsRolling == false)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }

    protected override void OnRoll(eActionPhase actionPhase)
    {

    }
}
