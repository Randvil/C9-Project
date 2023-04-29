using UnityEngine;

public abstract class BasePlayerState : IState
{
    protected Player player;
    protected IPlayerInput playerInput;
    protected IStateMachine stateMachine;

    protected static bool haveToTurn;
    protected static bool isMoving;
    protected static int abilityNumberToCast;
    protected static int previousAbilityNumber;

    public BasePlayerState(Player player, IStateMachine stateMachine, IPlayerInput playerInput)
    {
        this.player = player;
        this.playerInput = playerInput;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        playerInput.MoveEvent.AddListener(OnMove);
        playerInput.StopEvent.AddListener(OnStop);
        playerInput.CrouchEvent.AddListener(OnCrouch);
        playerInput.JumpEvent.AddListener(OnJump);
        playerInput.AttackEvent.AddListener(OnAttack);
        playerInput.RollEvent.AddListener(OnRoll);
        playerInput.ParryEvent.AddListener(OnParry);
        playerInput.InteractEvent.AddListener(OnInteract);
        playerInput.ClimbUpEvent.AddListener(OnClimbUp);
        playerInput.AbilityEvent.AddListener(OnAbilityUse);
        playerInput.ChangeAbilityLayoutEvent.AddListener(OnChangeAbilityLayout);

        player.EffectManager.EffectEvent.AddListener(OnStun);
        player.DeathManager.DeathEvent.AddListener(OnDeath);
    }

    public virtual void Exit()
    {
        playerInput.MoveEvent.RemoveListener(OnMove);
        playerInput.StopEvent.RemoveListener(OnStop);
        playerInput.CrouchEvent.RemoveListener(OnCrouch);
        playerInput.JumpEvent.RemoveListener(OnJump);
        playerInput.AttackEvent.RemoveListener(OnAttack);
        playerInput.RollEvent.RemoveListener(OnRoll);
        playerInput.ParryEvent.RemoveListener(OnParry);
        playerInput.InteractEvent.RemoveListener(OnInteract);
        playerInput.ClimbUpEvent.RemoveListener(OnClimbUp);
        playerInput.AbilityEvent.RemoveListener(OnAbilityUse);
        playerInput.ChangeAbilityLayoutEvent.RemoveListener(OnChangeAbilityLayout);

        player.EffectManager.EffectEvent.RemoveListener(OnStun);
        player.DeathManager.DeathEvent.RemoveListener(OnDeath);
    }

    public virtual void LogicUpdate()
    {
        if (player.Climb.CanClimb == true)
        {
            stateMachine.ChangeState(player.Climbing);
        }
    }

    public virtual void PhysicsUpdate()
    {

    }

    protected virtual void OnMove(eDirection direction)
    {
        haveToTurn = direction == player.Turning.Direction ? false : true;

        isMoving = true;
    }

    protected virtual void OnStop()
    {
        isMoving = false;
    }

    protected virtual void OnCrouch(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            stateMachine.ChangeState(player.Crouching);
        }
    }

    protected virtual void OnJump(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started && player.Jump.CanJump == true)
        {
            stateMachine.ChangeState(player.Jumping);
        }
    }

    protected virtual void OnRoll(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started && player.Roll.CanRoll == true)
        {
            stateMachine.ChangeState(player.Rolling);
        }
    }

    protected virtual void OnParry(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started && player.Parry.CanParry == true)
        {
            stateMachine.ChangeState(player.Parrying);
        }
    }

    protected virtual void OnAttack(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            stateMachine.ChangeState(player.Attacking);
        }
    }

    protected virtual void OnInteract(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started && player.Interact.CanInteract == true)
        {
            stateMachine.ChangeState(player.Interacting);
        }
    }

    protected virtual void OnClimbUp(eActionPhase actionPhase)
    {

    }

    protected virtual void OnAbilityUse(eActionPhase actionPhase, int abilityNumber)
    {
        if (actionPhase == eActionPhase.Started && player.AbilityManager.CanCastAbility(abilityNumber))
        {
            previousAbilityNumber = abilityNumberToCast;
            abilityNumberToCast = abilityNumber;
            stateMachine.ChangeState(player.CastingAbility);
        }
    }

    protected virtual void OnChangeAbilityLayout(eActionPhase actionPhase)
    {
        if (actionPhase == eActionPhase.Started)
        {
            player.AbilityManager.SwitchAbilityLayout();
        }
    }

    protected virtual void OnStun(eEffectType effectType, eEffectStatus effectStatus)
    {
        if (effectType == eEffectType.Stun && effectStatus == eEffectStatus.Added)
        {
            stateMachine.ChangeState(player.Stunned);
        }
    }

    protected virtual void OnDeath()
    {
        stateMachine.ChangeState(player.Dying);
    }
}
