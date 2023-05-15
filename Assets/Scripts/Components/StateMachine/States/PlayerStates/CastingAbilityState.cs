public class CastingAbilityState : BasePlayerState
{
    public CastingAbilityState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void Enter()
    {
        base.Enter();

        player.AbilityManager.StartCastAbility(playerInterstateData.abilityNumberToCast);
    }

    public override void Exit()
    {
        base.Exit();

        player.AbilityManager.BreakCastAbility(playerInterstateData.previousAbilityNumber);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.AbilityManager.IsPerforming(playerInterstateData.abilityNumberToCast) == false)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }

    protected override void OnAbilityUse(eActionPhase actionPhase, int abilityNumber)
    {
        if (abilityNumber == playerInterstateData.abilityNumberToCast)
        {
            if (actionPhase == eActionPhase.Canceled)
            {
                player.AbilityManager.StopSustainingAbility(playerInterstateData.abilityNumberToCast);
            }

            return;
        }

        base.OnAbilityUse(actionPhase, abilityNumber);
    }

    protected override void OnChangeAbilityLayout(eActionPhase actionPhase)
    {
        
    }
}
