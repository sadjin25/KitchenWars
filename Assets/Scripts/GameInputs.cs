using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    private PlayerInput playerInput;
    public event EventHandler OnInteractAction;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Inputs.Enable();
        playerInput.Inputs.Interact.performed += InteractPerformed;
    }

    public Vector2 GetMoveInputValue()
    {
        return playerInput.Inputs.Move.ReadValue<Vector2>();
    }

    void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
}
