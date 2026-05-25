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
    public event Action OnToggleFlashLight;
    public event Action OnStartFocus;
    public event Action OnEndFocus;
    public event Action OnPause;

    private void OnEnable()
    {
        myControls = new Controls();

        myControls.Gameplay.Enable();
        myControls.UI.Enable();

        // Movement input
        myControls.Gameplay.Move.started += Move;
        myControls.Gameplay.Move.performed += Move;
        myControls.Gameplay.Move.canceled += Move;

        // Jump input
        myControls.Gameplay.Jump.started += Jump;

        // Interact input
        myControls.Gameplay.Interact.started += Interact;

        // Toogle flashlight
        myControls.Gameplay.Toggle.started += ToogleFlashLight;

        // Focus input
        myControls.Gameplay.Focus.started += StartFocus;
        myControls.Gameplay.Focus.canceled += EndFocus;

        // Pause input
        myControls.UI.Pause.started += Pause;
    }

    private void OnDisable()
    {
        myControls.Gameplay.Disable();
        myControls.UI.Disable();

        // Movement input
        myControls.Gameplay.Move.started -= Move;
        myControls.Gameplay.Move.performed -= Move;
        myControls.Gameplay.Move.canceled -= Move;

        // Jump input
        myControls.Gameplay.Jump.started -= Jump;

        // Interact input
        myControls.Gameplay.Interact.started -= Interact;

        // Toogle flashlight
        myControls.Gameplay.Toggle.started -= ToogleFlashLight;

        // Focus input
        myControls.Gameplay.Focus.started -= StartFocus;
        myControls.Gameplay.Focus.canceled -= EndFocus;

        // Pause input
        myControls.UI.Pause.started -= Pause;
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

    private void ToogleFlashLight(InputAction.CallbackContext ctx)
    {
        OnToggleFlashLight?.Invoke();
    }

    private void StartFocus(InputAction.CallbackContext ctx)
    {
        OnStartFocus?.Invoke();
    }

    private void EndFocus(InputAction.CallbackContext ctx)
    {
        OnEndFocus?.Invoke();
    }

    private void Pause(InputAction.CallbackContext ctx)
    {
        OnPause?.Invoke();
    }
}
