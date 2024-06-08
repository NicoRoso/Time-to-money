using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Stand,
        Crouch
    }

    private PlayerState currentState = PlayerState.Stand;

    [Header("Crouch")]
    [SerializeField] private float standHeight = 2f;
    [SerializeField] private float crouchHeight = 1.5f;
    private Vector3 originalCameraPos;
    private bool canCrouch;
    private bool isCrouched;
    [SerializeField] private float maxHeightCheck = 2f;

    [Header("Input Action")]
    [SerializeField] private InputActionAsset playerInput;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField] private Camera mainCamera;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction sprintAction;

    private Vector2 moveVector;
    private Vector2 lookVector;

    [Header("Speed")]
    public float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 8f;
    private bool isSprinting;

    [Header("Look Sensitivity")]
    [SerializeField] float mouseSensitivity = 0.3f;
    private float upDownRange = 90f;

    [Header("Jump")]
    private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 1.5f;
    private Vector3 velocity;
    private bool isGrounded;

    public bool isMoving;
    private Vector3 currentMovement = Vector3.zero;

    private float verticalRotation;

    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip stepRunSound;
    public static Action<AudioClip> isWalking;
    public static Action<AudioClip> isRunning;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        mainCamera = Camera.main;

        moveAction = playerInput.FindActionMap("Player").FindAction("Movement");
        lookAction = playerInput.FindActionMap("Player").FindAction("View");
        jumpAction = playerInput.FindActionMap("Player").FindAction("Jump");
        crouchAction = playerInput.FindActionMap("Player").FindAction("Crouch");
        sprintAction = playerInput.FindActionMap("Player").FindAction("Sprint");

        moveAction.performed += context => moveVector = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveVector = Vector2.zero;

        lookAction.performed += context => lookVector = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookVector = Vector2.zero;

        crouchAction.performed += context => ToggleCrouch();

        sprintAction.performed += context => { if (!isCrouched) { isSprinting = true; } };
        sprintAction.canceled += context => isSprinting = false;

        originalCameraPos = mainCamera.transform.localPosition;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        crouchAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        crouchAction.Disable();
        sprintAction.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJumpAndGravity();
    }

    void HandleMovement()
    {
        float verticalSpeed = moveVector.y * (isSprinting ? sprintSpeed : walkSpeed);
        float horizontalSpeed = moveVector.x * (isSprinting ? sprintSpeed : walkSpeed);

        Vector3 horizontalMovement = new Vector3 (horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        characterController.Move(horizontalMovement * Time.deltaTime);

        if (horizontalMovement != Vector3.zero && isGrounded)
        {
            if (!isSprinting)
            {
                isWalking?.Invoke(stepSound);
            }
            else
            {
                isRunning?.Invoke(stepRunSound);
            }
        }

        isMoving = moveVector.y != 0 || moveVector.x != 0;
    }

    void HandleRotation()
    {
        float mouseXRotation = lookVector.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= lookVector.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }


    void HandleJumpAndGravity()
    {
        float raycastDistance = characterController.height * 0.5f + 0.1f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance);

        if (isGrounded && jumpAction.triggered)
        {
            velocity.y = Mathf.Sqrt(2 * jumpForce * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void ToggleCrouch()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, maxHeightCheck) && currentState == PlayerState.Crouch)
        {
            canCrouch = false;
        }
        else
        {
            canCrouch = true;

            if (currentState == PlayerState.Stand && canCrouch)
            {
                StartCoroutine(CrouchRoutine(crouchHeight, originalCameraPos.y));
                isCrouched = true;
            }
            else if (currentState == PlayerState.Crouch)
            {
                StartCoroutine(CrouchRoutine(standHeight, originalCameraPos.y));
                isCrouched = false;
            }
        }
    }

    IEnumerator CrouchRoutine(float targetHeight, float targetCameraY)
    {
        float duration = 0.2f;
        float originalHeight = characterController.height;
        Vector3 originalCameraPos = mainCamera.transform.localPosition;
        float timer = 0f;

        while (timer < duration)
        {
            float t = timer / duration;
            characterController.height = Mathf.Lerp(originalHeight, targetHeight, t);
            mainCamera.transform.localPosition = Vector3.Lerp(originalCameraPos, new Vector3(originalCameraPos.x, targetCameraY, originalCameraPos.z), t);
            timer += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        mainCamera.transform.localPosition = new Vector3(originalCameraPos.x, targetCameraY, originalCameraPos.z);

        currentState = (targetHeight == crouchHeight) ? PlayerState.Crouch : PlayerState.Stand;
    }
}
