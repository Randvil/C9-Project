using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ITeam, IDataSavable
{
    private eTeam team = eTeam.Player;
    public eTeam Team { get => team; }

    private bool intendToMove;
    private bool isAlive = true;

    private IPlayerInput playerInput;
    private IMovement movement;
    private IJumping jump;
    private IRoll roll;
    private ITurning turning;
    private IWeapon weapon;
    private IStats stats;
    private IDamageHandler damageHandler;
    private IParry parry;
    private IUIComponent ui;
    private IClimb climb;
    private IInteract interact;

    private void Awake()
    {
        stats = GetComponent<IStats>();
    }

    private void Start()
    {
        playerInput = GetComponent<IPlayerInput>();
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJumping>();
        roll = GetComponent<IRoll>();
        turning = GetComponent<ITurning>();
        weapon = GetComponent<IWeapon>();
        
        damageHandler = GetComponent<IDamageHandler>();
        parry = GetComponent<IParry>();
        ui = GetComponent<IUIComponent>();
        climb = GetComponent<IClimb>();
        interact = GetComponent<IInteract>();

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
        playerInput.InteractEvent.AddListener(OnInteract);
        playerInput.ClimbEvent.AddListener(OnClimb);
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
            jump.HandleJump();
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

        DeathLoad deathData = new();
        deathData.RewriteData();
        deathData.LoadCheckpoint();

        /*movement.StopMove();
        weapon.StopAttack();
        roll.StopRoll();
        parry.StopParry();

        isAlive = false;*/
    }

    private void OnInteract(eActionPhase actionPhase)
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

    public void SaveData(Data data)
    {
        data.playerHealth = stats.GetStat(eStatType.CurrentHealth);
        data.position = transform.position;
    }

}
