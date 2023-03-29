using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : MovableState
{
    public CrouchingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput) : base(player, stateMachine, playerInput) { }

    public override void Enter()
    {
        base.Enter();

        player.Crouch.StartCrouch();
        player.CrouchView.StartCrouch();
    }

    public override void Exit()
    {
        base.Exit();

        player.Crouch.BreakCrouch();
        player.CrouchView.BreakCrouch();
    }

    protected override void OnCrouch(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Canceled)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }
}
