using UnityEngine;

public class DyingState : BasePlayerState
{
    public DyingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.DeathView.StartDying();

        Debug.Log("You're dying :(");
    }

    public override void Exit()
    {
        base.Exit();

        player.DeathView.BreakDying();
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    protected override void OnMove(eDirection direction)
    {

    }

    protected override void OnStop()
    {

    }

    protected override void OnCrouch(eActionPhase actionPhase)
    {

    }

    protected override void OnJump(eActionPhase actionPhase)
    {

    }

    protected override void OnRoll(eActionPhase actionPhase)
    {

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

    protected override void OnStun(eEffectType effectType, eEffectStatus effectStatus)
    {

    }

    protected override void OnDeath()
    {

    }
}
