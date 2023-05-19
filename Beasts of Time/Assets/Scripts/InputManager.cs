using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnInteractAction;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        // Subscribe to relevant events from input actions
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Interact.performed += Interact_performed;

        // Create singleton instance
        if (Instance != null)
        {
            Debug.LogError("There is more than one InputManager instance");
        }
        Instance = this;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetHorizontalMovementVector()
    {
        Vector2 inputVector = new Vector2(0, 0);

        // Check for horizontal movement inputs
        inputVector.x = inputActions.Player.HorizontalMove.ReadValue<float>();

        return inputVector;
    }
}
