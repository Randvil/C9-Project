using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : BasePlayerState
{
    public StunnedState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
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
        if (effectType == eEffectType.Stun && effectStatus == eEffectStatus.Cleared)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }
}
