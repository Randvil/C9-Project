using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemListener : MonoBehaviour, IPlayerInput
{
    [SerializeField]
    private PlayerInput unityInputSystem;

    private UnityEvent<eDirection> moveEvent = new();
    private UnityEvent stopEvent = new();
    private UnityEvent<eActionPhase> jumpEvent = new();
    private UnityEvent<eActionPhase> attackEvent = new();
    private UnityEvent<eActionPhase> rollEvent = new();
    private UnityEvent<eActionPhase, eAbilityType> abilityEvent = new();
    private UnityEvent<eActionPhase> parryEvent = new();
    private UnityEvent<eActionPhase> interactEvent = new();
    private UnityEvent<int> climbEvent = new();

    public UnityEvent<eDirection> MoveEvent { get => moveEvent; }
    public UnityEvent StopEvent { get => stopEvent; }
    public UnityEvent<eActionPhase> JumpEvent { get => jumpEvent; }
    public UnityEvent<eActionPhase> AttackEvent { get => attackEvent; }
    public UnityEvent<eActionPhase> RollEvent { get => rollEvent; }
    public UnityEvent<eActionPhase, eAbilityType> AbilityEvent { get => abilityEvent; }
    public UnityEvent<eActionPhase> ParryEvent { get => parryEvent; }
    public UnityEvent<eActionPhase> InteractEvent { get => interactEvent; }
    public UnityEvent<int> ClimbEvent { get => climbEvent; }

    private void Awake()
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
                    moveEvent.Invoke(eDirection.Right);
                else if (moveCommand < 0)
                    moveEvent.Invoke(eDirection.Left);
                else
                    stopEvent.Invoke();

                break;

            case "Jump":

                if (context.action.phase == InputActionPhase.Started)
                    jumpEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    jumpEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Attack":

                if (context.action.phase == InputActionPhase.Started)
                    attackEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    attackEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Roll":

                if (context.action.phase == InputActionPhase.Started)
                    rollEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    rollEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Parry":

                if (context.action.phase == InputActionPhase.Started)
                    parryEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    parryEvent.Invoke(eActionPhase.Canceled);

                break;

            case "Interact":

                if (context.action.phase == InputActionPhase.Started) 
                    interactEvent.Invoke(eActionPhase.Started);
                else if (context.action.phase == InputActionPhase.Canceled)
                    interactEvent.Invoke(eActionPhase.Canceled);

                break;
            case "Climb":

                float climbCommand = context.action.ReadValue<float>();

                if (climbCommand > 0)
                    climbEvent.Invoke(1);
                else if (climbCommand < 0)
                    climbEvent.Invoke(-1);
                else if (context.action.phase == InputActionPhase.Performed)
                    climbEvent.Invoke(0);
                break;
        }
    }
}
