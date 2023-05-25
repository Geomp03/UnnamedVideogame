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
    public event Action<bool> OnRewindAction;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        // Subscribe to relevant events from input actions
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.Rewind.started += Rewind_started;
        inputActions.Player.Rewind.canceled += Rewind_canceled;

        // Create InputManager singleton instance
        if (Instance != null)
        {
            Debug.LogError("There is more than one InputManager instance");
        }
        Instance = this;
    }

    // Player rewind input event
    private void Rewind_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Rewind canceled");
        OnRewindAction?.Invoke(false);
    }

    private void Rewind_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Rewind started");
        OnRewindAction?.Invoke(true);
    }

    // Interact button event
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    // Jump input event
    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    // Horizontal movement input event
    public Vector2 GetHorizontalMovementVector()
    {
        Vector2 inputVector = new Vector2(0, 0);

        // Check for horizontal movement inputs
        inputVector.x = inputActions.Player.HorizontalMove.ReadValue<float>();

        return inputVector;
    }
}
