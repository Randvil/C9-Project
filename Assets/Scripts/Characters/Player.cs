using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITeam
{
    private eTeam team = eTeam.Player;
    public eTeam Team { get => team; }

    private bool intendToMove;
    private bool isAlive = true;

    private IPlayerInput playerInput;
    private IMovement movement;
    private IJump jump;
    private IRoll roll;
    private ITurning turning;
    private IWeapon weapon;
    private IStats stats;
    private IDamageHandler damageHandler;
    private IParry parry;
    private IUIComponent ui;

    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJump>();
        roll = GetComponent<IRoll>();
        turning = GetComponent<ITurning>();
        weapon = GetComponent<IWeapon>();
        stats = GetComponent<IStats>();
        damageHandler = GetComponent<IDamageHandler>();
        parry = GetComponent<IParry>();
        ui = GetComponent<IUIComponent>();

        AddInputListeners();

        stats.ChangeStatEvent.AddListener(OnDie);
    }

    private void Update()
    {
        HandleMoveInput();
    }

    private void AddInputListeners()
    {
        playerInput.MoveEvent.AddListener(OnMove);
        playerInput.StopEvent.AddListener(OnStop);
        playerInput.JumpEvent.AddListener(OnJump);
        playerInput.AttackEvent.AddListener(OnAttack);
        playerInput.RollEvent.AddListener(OnRoll);
        playerInput.ParryEvent.AddListener(OnParry);
    }

    private void HandleMoveInput()
    {
        if (!isAlive || roll.IsRolling || weapon.IsAttacking || parry.IsParrying)
            return;

        if (intendToMove)
            movement.StartMove(turning.Direction);
        else
            movement.StopMove();
    }

    private void OnMove(eDirection direction)
    {
        intendToMove = true;
        turning.Turn(direction);
    }

    private void OnStop()
    {
        intendToMove = false;
    }

    private void OnJump(eActionPhase actionPhase)
    {
        if (isAlive && !jump.IsJumping)
        {
            weapon.StopAttack();
            roll.StopRoll();
            parry.StopParry();

            jump.StartJump();
        }
    }

    private void OnAttack(eActionPhase actionPhase)
    {
        if (isAlive && actionPhase == eActionPhase.Started)
        {
            movement.StopMove();
            roll.StopRoll();
            parry.StopParry();

            weapon.StartAttack();
        }
    }

    private void OnRoll(eActionPhase actionPhase)
    {
        if (isAlive && actionPhase == eActionPhase.Started && !roll.IsRolling)
        {
            movement.StopMove();
            weapon.StopAttack();
            parry.StopParry();

            roll.StartRoll(turning.Direction);
        }
            
    }
    private void OnParry(eActionPhase actionPhase)
    {
        if (!isAlive && parry.IsOnCooldown)
            return;

        switch (actionPhase)
        {
            case eActionPhase.Started:
                movement.StopMove();
                weapon.StopAttack();
                roll.StopRoll();
                parry.StartParry(turning.Direction);
                break;

            case eActionPhase.Canceled:
                parry.StopParry();
                break;
        }
    }

    private void OnDie(eStatType stat, float value)
    {
        if (stat != eStatType.CurrentHealth || value != 0f)
            return;

        Destroy(gameObject, 1f);

        movement.StopMove();
        weapon.StopAttack();
        roll.StopRoll();
        parry.StopParry();

        isAlive = false;
    }
}
