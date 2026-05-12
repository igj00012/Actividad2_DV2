using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManager")]
public class InputManagerSO : ScriptableObject
{
    Controls myControls;

    public event Action<Vector2> OnMove;
    public event Action OnJump;
    public event Action OnInteract;

    private void OnEnable()
    {
        myControls = new Controls();

        myControls.Gameplay.Enable();

        // Movement input
        myControls.Gameplay.Move.started += Move;
        myControls.Gameplay.Move.performed += Move;
        myControls.Gameplay.Move.canceled += Move;

        // Jump input
        myControls.Gameplay.Jump.started += Jump;

        // Interact input
        myControls.Gameplay.Interact.started += Interact;
    }

    private void OnDisable()
    {
        myControls.Gameplay.Disable();

        // Movement input
        myControls.Gameplay.Move.started -= Move;
        myControls.Gameplay.Move.performed -= Move;
        myControls.Gameplay.Move.canceled -= Move;

        // Jump input
        myControls.Gameplay.Jump.started -= Jump;

        // Interact input
        myControls.Gameplay.Interact.started -= Interact;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke();
    }
}
