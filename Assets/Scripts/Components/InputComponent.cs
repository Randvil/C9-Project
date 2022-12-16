using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public GameInput gameInput;
    public bool isJumpPressed;
    public Vector2 vectorMove;

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
    }

    private void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void onMove(InputAction.CallbackContext context)
    {
        vectorMove = context.ReadValue<Vector2>();
    }

}
