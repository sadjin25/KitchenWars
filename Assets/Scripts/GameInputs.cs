using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance { get; private set; }

    private PlayerInput playerInput;
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseAction;

    void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();
        playerInput.Inputs.Enable();
        playerInput.Inputs.Interact.performed += InteractPerformed;
        playerInput.Inputs.Pause.performed += PausePerformed;
    }

    void OnDestroy()
    {
        //what are you doing?: on scene changing, we can't clean up PlayerInput instance.
        //so, dispose current playerInput, and build new one when Awake() on after's Scene.
        playerInput.Inputs.Interact.performed -= InteractPerformed;
        playerInput.Inputs.Pause.performed -= PausePerformed;

        playerInput.Dispose();
    }

    public Vector2 GetMoveInputValue()
    {
        return playerInput.Inputs.Move.ReadValue<Vector2>();
    }

    void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
}
