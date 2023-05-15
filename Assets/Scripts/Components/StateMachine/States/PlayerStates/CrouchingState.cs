using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : MovableState
{
    public CrouchingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void Enter()
    {
        base.Enter();

        player.Crouch.StartCrouch();
    }

    public override void Exit()
    {
        base.Exit();

        player.Crouch.BreakCrouch();
    }

    protected override void OnCrouch(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Canceled)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }
}
