using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera playerCamera;
    private CharacterController _characterController;
    private Vector3 moveDirections;
    private Vector2 currentInput;
    public bool CanMove { get; private set; } = true;
    
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && _characterController.isGrounded;
    
    [Header("Controls")] 
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interactionKey = KeyCode.F;

    
    [Header("Look Parameters")] 
    [SerializeField, Range(1, 10)] private float lookSpeedX = 5f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 5f;
    [SerializeField, Range(1, 100)] private float upperLookLimit = 80f;
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 80f;
    private float rotationX = 0f;
    
    [Header("Movement Parameters")] 
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float sprintSpeed = 6f;
    
    [Header("Jumping Parameters")] 
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 30f;
    
    [Header("Functional Options")] 
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canInteract = true;
    [Header("Interaction Parameters")]
    [SerializeField] private Vector3 interactionRaycastOrigin = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayerMask = default;
    private Interactable currentInteracable;
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLock();

            if (canJump)
            {
                HandleJump();
            }
            
            ApplyFinalMovement();
        }

        if (canInteract)
        {
            HandleInteractionCheck();
            HandleInteractionInput();
        }
        
    }

    void HandleMovementInput() 
    {
        currentInput = new Vector2((isSprinting ? sprintSpeed : moveSpeed) * Input.GetAxis("Vertical"), (isSprinting ? sprintSpeed : moveSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirections.y;

        moveDirections = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right)* currentInput.y);
        moveDirections.y = moveDirectionY;
    }
    
    void HandleMouseLock()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX,0,0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X")*lookSpeedX,0);
    }
    
    void HandleJump()
    {
        if (ShouldJump)
        {
            moveDirections.y = jumpForce;
        }
    }
    
    void ApplyFinalMovement()
    {
        if (!_characterController.isGrounded)
        {
            moveDirections.y -= gravity * Time.deltaTime;
        }

        _characterController.Move(moveDirections * Time.deltaTime);
    }

    void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRaycastOrigin), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 9 && (currentInteracable == null || hit.collider.gameObject.GetInstanceID() != currentInteracable.GetInstanceID()))
            {
                hit.collider.gameObject.TryGetComponent(out currentInteracable);

                if (currentInteracable)
                {
                    currentInteracable.OnFocus();
                }
            }

            else if (currentInteracable)
            {
                currentInteracable.OnLoseFocus();
                
                currentInteracable = null;
            }
        }
    }

    void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactionKey) && currentInteracable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRaycastOrigin), out RaycastHit hit, interactionDistance, interactionLayerMask))
        {
            currentInteracable.OnInteract();
        }
    }
}
