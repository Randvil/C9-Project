using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputSystemListener))]
[RequireComponent(typeof(RigidbodyMovement), typeof(Jump), typeof(Turning))]
[RequireComponent(typeof(SingleTargetMeleeWeapon), typeof(Stats), typeof(DamageHandlerPlayer))]
[RequireComponent(typeof(Roll), typeof(Parry), typeof(Gravity))]
[RequireComponent(typeof(AnimationPlayerController), typeof(RigidbodyClimb), typeof(Interact))]
public class Player : MonoBehaviour, ITeam
{
    private eTeam team = eTeam.Player;
    public eTeam Team { get => team; }

    private bool moveRight;
    private bool moveLeft;
    private bool isAlive = true;

    private IPlayerInput playerInput;
    private IMovement movement;
    private IJumping jumping;
    private IRoll roll;
    private ITurning turning;
    private IWeapon weapon;
    private IStats stats;
    private IDamageHandler damageHandler;
    private IParry parry;
    private IClimb climb;
    private IInteract interact;

    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        jumping = GetComponent<IJumping>();
        roll = GetComponent<IRoll>();
        turning = GetComponent<ITurning>();
        weapon = GetComponent<IWeapon>();
        stats = GetComponent<IStats>();
        damageHandler = GetComponent<IDamageHandler>();
        parry = GetComponent<IParry>();
        climb = GetComponent<IClimb>();
        interact = GetComponent<IInteract>();

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
        playerInput.InteractEvent.AddListener(OnInteract);
        playerInput.ClimbEvent.AddListener(OnClimb);
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

        eDirection direction = eDirection.Right;
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
        if (isAlive && !jumping.IsJumping)
        {
            roll.StopRoll();
            weapon.StopAttack();
            jumping.HandleJump();
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
        switch (turning.Direction)
        {
            case eDirection.Right:
                parry.StartParry(Vector3.right);
                break;
            case eDirection.Left:
                parry.StartParry(Vector3.left);
                break;
        }
        
    }

    private void OnInteract()
    {
        Debug.Log(interact.CheckInteractiveObjectsNear());
    }

    private void OnClimb(int dir)
    {
        GameObject interactiveObject = interact.CheckInteractiveObjectsNear();
        if (interactiveObject != null && interactiveObject.GetComponent<Ladder>() != null)
        {
            climb.HandleClimb(dir, interactiveObject.GetComponent<Ladder>());
        }
    }
}
