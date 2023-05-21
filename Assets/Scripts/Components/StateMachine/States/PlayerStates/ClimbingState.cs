using UnityEngine;

public class ClimbingState : BasePlayerState
{
    public ClimbingState(Player player, IStateMachine stateMachine, IPlayerInput playerInput, PlayerInterstateData playerInterstateData) : base(player, stateMachine, playerInput, playerInterstateData) { }

    public override void Enter()
    {
        base.Enter();

        player.Climb.StartClimb();
        if (player.Climb.HaveToTurn)
        {
            eDirection newDirection = player.Turning.Direction == eDirection.Right ? eDirection.Left : eDirection.Right;
            player.Turning.Turn(newDirection);
            playerInterstateData.haveToTurn = true;
        }

        //player.Gravity.SetFallingState();

        if (playerInterstateData.climbUp)
        {
            player.Climb.ClimbUp();
        }
        else if (playerInterstateData.climbDown)
        {
            player.Climb.ClimbDown();
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.Climb.BreakClimb();

        //player.Gravity.SetFallingState();
    }

    public override void LogicUpdate()
    {
        if (player.Climb.CanClimb == false)
        {
            stateMachine.ChangeState(player.Standing);
        }

        //if (player.Gravity.IsGrounded && player.Climb.ClimbSpeed < 0f)
        //{
        //    player.Climb.StopClimb();
        //}
    }

    protected override void OnCrouch(eActionPhase actionPhase)
    {
        base.OnCrouch(actionPhase);

        if (playerInterstateData.climbDown)
        {
            player.Climb.ClimbDown();
        }
        else
        {
            player.Climb.StopClimb();
        }
    }

    protected override void OnJump(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            stateMachine.ChangeState(player.Jumping);
        }
    }

    protected override void OnParry(eActionPhase actionPhase)
    {

    }

    protected override void OnAttack(eActionPhase actionPhase)
    {

    }

    protected override void OnRoll(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            stateMachine.ChangeState(player.Standing);
        }
    }

    protected override void OnClimbUp(eActionPhase actionPhase)
    {
        base.OnClimbUp(actionPhase);

        if (playerInterstateData.climbUp)
        {
            player.Climb.ClimbUp();
        }
        else
        {
            player.Climb.StopClimb();
        }
    }

    protected override void OnAbilityUse(eActionPhase actionPhase, int abilityNumber)
    {

    }
}
