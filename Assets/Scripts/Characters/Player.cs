using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputSystemListener))]
[RequireComponent(typeof(RigidbodyMovement), typeof(ParabolicJump), typeof(Turning))]
[RequireComponent(typeof(SingleTargetMeleeWeapon), typeof(Stats), typeof(DamageHandlerPlayer))]
[RequireComponent(typeof(Roll), typeof(Parry))]
public class Player : MonoBehaviour, ITeam
{
    private eTeam team = eTeam.Player;

    private eDirection direction;
    public eTeam Team { get => team; }

    private bool moveRight;
    private bool moveLeft;
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
        HandleMovement();
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

    private void OnMove(eDirection direction)
    {
        switch (direction)
        {
            case eDirection.Right:
                moveRight = true;
                moveLeft = false;
                break;

            case eDirection.Left:
                moveRight = false;
                moveLeft = true;
                break;
        }
    }

    private void OnStop()
    {
        moveRight = false;
        moveLeft = false;
    }

    private void HandleMovement()
    {
        if (!isAlive || roll.IsRolling || weapon.IsAttacking || parry.IsParrying)
            return;

        if (!moveRight && !moveLeft)
        {
            movement.StopMove();
            return;
        }

        //eDirection
        direction = eDirection.Right;
        if (moveLeft)
            direction = eDirection.Left;

        movement.StartMove(direction);

        if (direction != turning.Direction)
        {
            turning.Turn(direction);
        }
    }

    private void OnJump()
    {
        if (isAlive && !jump.IsJumping)
        {
            roll.StopRoll();
            weapon.StopAttack();
            jump.StartJump();
        }
    }

    private void OnAttack()
    {
        if (isAlive && !weapon.IsAttacking)
        {
            movement.StopMove();
            roll.StopRoll();
            weapon.StartAttack(turning.Direction);
        }
    }

    private void OnRoll()
    {
        if (isAlive && !roll.IsRolling)
        {
            movement.StopMove();
            weapon.StopAttack();
            roll.StartRoll(turning.Direction);
        }
            
    }

    private void OnDie(eStatType stat, float value)
    {
        if (stat != eStatType.CurrentHealth || value != 0f)
            return;

        Destroy(gameObject, 1f);

        weapon.StopAttack();
        movement.StopMove();
        roll.StopRoll();

        isAlive = false;
    }

    private void OnParry()
    {
        switch (direction)
        {
            case eDirection.Right:
                parry.StartParry(Vector3.right);
                break;
            case eDirection.Left:
                parry.StartParry(Vector3.left);
                break;
        }
        
    }
}
