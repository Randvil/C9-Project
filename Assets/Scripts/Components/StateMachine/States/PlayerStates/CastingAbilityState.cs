using UnityEditorInternal;

public class CastingAbilityState : BasePlayerState
{
    public CastingAbilityState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.AbilityManager.StartCastAbility(abilityNumberToCast);
    }

    public override void Exit()
    {
        base.Exit();

        player.AbilityManager.BreakCastAbility(abilityNumberToCast);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.AbilityManager.IsPerforming(abilityNumberToCast) == false)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }

    protected override void OnAbilityUse(eActionPhase actionPhase, int abilityNumber)
    {
        if (abilityNumber == abilityNumberToCast)
        {
            if (actionPhase == eActionPhase.Canceled)
            {
                player.AbilityManager.StopSustainingAbility(abilityNumberToCast);
            }

            return;
        }

        base.OnAbilityUse(actionPhase, abilityNumber);
    }

    protected override void OnChangeAbilityLayout(eActionPhase actionPhase)
    {
        
    }
}