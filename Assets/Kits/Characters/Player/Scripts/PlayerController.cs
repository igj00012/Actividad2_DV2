using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [SerializeField] Transform camTransform;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravityFactor = -9.8f;
    [SerializeField] Transform foot;
    [SerializeField] LayerMask whatIsFloor;
    [SerializeField] float sphereRadius = 0.1f;

    [Header("Inputs")]
    [SerializeField] InputManagerSO inputManager;

    [Header("Gizmos")]
    [SerializeField] bool showGizmos = false;

    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputManager.OnMove += GetMoveDirectionFromInput;
        inputManager.OnJump += Jump;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        Move();

        RotateToCameraForward();

        CheckGravity();
    }

    private void OnDisable()
    {
        inputManager.OnMove -= GetMoveDirectionFromInput;
        inputManager.OnJump -= Jump;
    }

    Vector3 inputDirection = Vector3.zero;
    private void GetMoveDirectionFromInput(Vector2 ctx)
    {
        inputDirection = new Vector3(ctx.x, 0, ctx.y);
    }

    private void Move()
    {
        moveDirection = (camTransform.forward * inputDirection.z + camTransform.right * inputDirection.x).normalized;
        moveDirection.y = 0;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

    void RotateToCameraForward()
    {
        Vector3 cameraForward = camTransform.forward;
        cameraForward.y = 0;

        if (cameraForward.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    bool IsInFloor()
    {
        return Physics.CheckSphere(foot.position, sphereRadius, whatIsFloor);
    }

    [SerializeField] Vector3 verticalVelocity = Vector3.zero;
    void Jump()
    {
        if (IsInFloor())
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * gravityFactor * jumpHeight);
        }
    }

    private void CheckGravity()
    {
        if (IsInFloor() && verticalVelocity.y < 0)
        {
            verticalVelocity.y = 0;
        }

        ApplyGravity();
    }

    void ApplyGravity()
    {
        verticalVelocity.y += gravityFactor * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(foot.position, sphereRadius);
        }
    }
}
