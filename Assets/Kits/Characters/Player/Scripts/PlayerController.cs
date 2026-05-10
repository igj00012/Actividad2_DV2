using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [SerializeField] Transform camera;

    [Header("Inputs")]
    [SerializeField] InputActionReference moveInput;

    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveInput.action.Enable();
        moveInput.action.started += OnMove;
        moveInput.action.performed += OnMove;
        moveInput.action.canceled += OnMove;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        moveDirection = camera.forward * inputDirection.z + camera.right * inputDirection.x;
        moveDirection.y = 0;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (moveDirection.sqrMagnitude > 0)
        {
            RotateToDestiny();
        }
    }

    private void OnDisable()
    {
        moveInput.action.Disable();
        moveInput.action.started -= OnMove;
        moveInput.action.performed -= OnMove;
        moveInput.action.canceled -= OnMove;
    }

    Vector3 inputDirection = Vector3.zero;
    private void OnMove(InputAction.CallbackContext ctx)
    {
        inputDirection = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
    }

    void RotateToDestiny()
    {
        Quaternion newRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = newRotation;
    }
}
