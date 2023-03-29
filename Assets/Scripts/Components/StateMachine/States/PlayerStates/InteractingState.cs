public class InteractingState : BasePlayerState
{
    public InteractingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.Interact.StartInteraction();
    }

    public override void Exit()
    {
        base.Exit();

        player.Interact.BreakInteraction();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.Interact.IsInteracting == false)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }

    protected override void OnCrouch(eActionPhase actionPhase)
    {

    }

    protected override void OnJump(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            player.Interact.NextStep();
        }
    }

    protected override void OnRoll(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }

    protected override void OnParry(eActionPhase actionPhase)
    {

    }

    protected override void OnAttack(eActionPhase actionPhase)
    {

    }

    protected override void OnInteract(eActionPhase actionPhase)
    {

    }

    protected override void OnClimbUp(eActionPhase actionPhase)
    {

    }

    protected override void OnAbilityUse(eActionPhase actionPhase, int abilityNumber)
    {

    }
}
