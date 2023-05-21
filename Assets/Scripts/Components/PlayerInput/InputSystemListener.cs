using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemListener : IPlayerInput
{
    private PlayerInput unityInputSystem;

    public UnityEvent<eDirection> MoveEvent { get; } = new();
    public UnityEvent StopEvent { get; } = new();
    public UnityEvent<eActionPhase> CrouchEvent { get; } = new();
    public UnityEvent<eActionPhase> JumpEvent { get; } = new();
    public UnityEvent<eActionPhase> AttackEvent { get; } = new();
    public UnityEvent<eActionPhase> RollEvent { get; } = new();
    public UnityEvent<eActionPhase> ParryEvent { get; } = new();
    public UnityEvent<eActionPhase> InteractEvent { get; } = new();
    public UnityEvent<eActionPhase> ClimbUpEvent { get; } = new();
    public UnityEvent<eActionPhase, int> AbilityEvent { get; } = new();
    public UnityEvent<eActionPhase> ChangeAbilityLayoutEvent { get; } = new();
    public UnityEvent ToggleMoveSpeedCheatEvent { get; } = new();
    public UnityEvent ToggleIncreaseWeaponDamageCheatEvent { get; } = new();
    public UnityEvent ToggleDecreaseTakenDamageCheatEvent { get; } = new();
    public UnityEvent ToggleCountlessJumpsCheatEvent { get; } = new();

    public InputSystemListener(PlayerInput unityInputSystem)
    {
        this.unityInputSystem = unityInputSystem;

        Initialize();
    }

    private void Initialize()
    {
        unityInputSystem.onActionTriggered += OnPlayerInputActionTriggered;
    }

    private void OnPlayerInputActionTriggered(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":

                float moveCommand = context.action.ReadValue<float>();

                if (moveCommand > 0)
                    MoveEvent.Invoke(eDirection.Right);
                else if (moveCommand < 0)
                    MoveEvent.Invoke(eDirection.Left);
                else
                    StopEvent.Invoke();

                break;

            case "Crouch":

                if (context.action.phase == InputActionPhase.Started)
                    CrouchEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    CrouchEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Jump":

                if (context.action.phase == InputActionPhase.Started)
                    JumpEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    JumpEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Attack":

                if (context.action.phase == InputActionPhase.Started)
                    AttackEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    AttackEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Roll":

                if (context.action.phase == InputActionPhase.Started)
                    RollEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    RollEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Parry":

                if (context.action.phase == InputActionPhase.Started)
                    ParryEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    ParryEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Interact":

                if (context.action.phase == InputActionPhase.Started) 
                    InteractEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    InteractEvent.Invoke(eActionPhase.Canceled);

                break;

            case "ClimbUp":

                if (context.action.phase == InputActionPhase.Started)
                    ClimbUpEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    ClimbUpEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Ability1":

                if (context.action.phase == InputActionPhase.Started)
                    AbilityEvent.Invoke(eActionPhase.Started, 1);
                else if (context.action.phase == InputActionPhase.Canceled)
                    AbilityEvent.Invoke(eActionPhase.Canceled, 1);

                break;

            case "Ability2":

                if (context.action.phase == InputActionPhase.Started)
                    AbilityEvent.Invoke(eActionPhase.Started, 2);
                else if (context.action.phase == InputActionPhase.Canceled)
                    AbilityEvent.Invoke(eActionPhase.Canceled, 2);

                break;

            case "ChangeAbilityLayout":
                if (context.action.phase == InputActionPhase.Started)
                    ChangeAbilityLayoutEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    ChangeAbilityLayoutEvent.Invoke(eActionPhase.Canceled);

                break;

            case "MoveSpeedCheat":
                if (context.action.phase == InputActionPhase.Started)
                    ToggleMoveSpeedCheatEvent.Invoke();

                break;

            case "WeaponDamageCheat":
                if (context.action.phase == InputActionPhase.Started)
                    ToggleIncreaseWeaponDamageCheatEvent.Invoke();

                break;

            case "TakenDamageCheat":
                if (context.action.phase == InputActionPhase.Started)
                    ToggleDecreaseTakenDamageCheatEvent.Invoke();

                break;

            case "CountlessJumpsCheat":
                if (context.action.phase == InputActionPhase.Started)
                    ToggleCountlessJumpsCheatEvent.Invoke();

                break;

        }
    }
}
