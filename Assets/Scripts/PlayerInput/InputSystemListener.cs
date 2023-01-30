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
    private UnityEvent jumpEvent = new();
    private UnityEvent attackEvent = new();
    private UnityEvent rollEvent = new();
    private UnityEvent<eAbilityType> abilityEvent = new();
    private UnityEvent parryEvent = new();
    private UnityEvent interactEvent = new();
    private UnityEvent<int> climbEvent = new();

    public UnityEvent<eDirection> MoveEvent { get => moveEvent;}
    public UnityEvent StopEvent { get => stopEvent;}
    public UnityEvent JumpEvent { get => jumpEvent; }
    public UnityEvent AttackEvent { get => attackEvent; }
    public UnityEvent RollEvent { get => rollEvent; }
    public UnityEvent<eAbilityType> AbilityEvent { get => abilityEvent; }
    public UnityEvent ParryEvent { get => parryEvent; }
    public UnityEvent InteractEvent { get => interactEvent; }
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
                {
                    jumpEvent.Invoke();
                }

                break;

            case "Attack":

                if (context.action.phase == InputActionPhase.Started)
                    attackEvent.Invoke();

                break;

            case "Roll":

                if (context.action.phase == InputActionPhase.Started)
                    rollEvent.Invoke();

                break;

            case "Parry":

                if (context.action.phase == InputActionPhase.Performed)
                    parryEvent.Invoke();

                break;
            case "Interact":

                if (context.action.phase == InputActionPhase.Started)
                {
                    interactEvent.Invoke();
                }

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
