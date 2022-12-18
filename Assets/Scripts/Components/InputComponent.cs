using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputComponent : MonoBehaviour
{
    public GameInput gameInput;
    public bool isJumpPressed;
    public bool isAttackPressed;
    public Vector2 vectorMove;
    public UnityEvent<Vector2, bool> changePositionEvent;
    public UnityEvent<Vector2> jumpButtonIsPressedEvent;
    public UnityEvent<Vector2> movingOnHorizontalEvent;
    public UnityEvent attackButtonIsPressedEvent;

    public void OnEnable()
    {
        gameInput.GameControls.Enable();
    }

    public void OnDisable()
    {
        gameInput.GameControls.Disable();
    }

    public void Awake()
    {
        gameInput = new GameInput();

        gameInput.GameControls.Jump.started += onJump;
        gameInput.GameControls.Jump.canceled += onJump;
        gameInput.GameControls.Move.started += onMove;
        gameInput.GameControls.Move.performed += onMove;
        gameInput.GameControls.Move.canceled += onMove;
        gameInput.GameControls.Attack.started += onAttack;
        gameInput.GameControls.Attack.canceled += onAttack;
    }

    public void Update()
    {
        changePositionEvent.Invoke(vectorMove, isJumpPressed);
    }

    public void Start()
    {
        if (jumpButtonIsPressedEvent == null)
            jumpButtonIsPressedEvent = new UnityEvent<Vector2>();
        if (movingOnHorizontalEvent == null)
            movingOnHorizontalEvent = new UnityEvent<Vector2>();
        if (attackButtonIsPressedEvent == null)
            attackButtonIsPressedEvent = new UnityEvent();
        if (changePositionEvent == null)
            changePositionEvent = new UnityEvent<Vector2, bool>();
    }

    private void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        if (isJumpPressed) jumpButtonIsPressedEvent.Invoke(vectorMove);
    }

    private void onMove(InputAction.CallbackContext context)
    {
        vectorMove = context.ReadValue<Vector2>();
        movingOnHorizontalEvent.Invoke(vectorMove);
    }

    private void onAttack(InputAction.CallbackContext context)
    {
        isAttackPressed = context.ReadValueAsButton();
        if (isAttackPressed) attackButtonIsPressedEvent.Invoke();
    }

}
