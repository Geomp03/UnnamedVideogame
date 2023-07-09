using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D playerRB;
    private InputManager inputManager;

    [SerializeField] private float walkingSpeed = 3f;
    [SerializeField] private float runningSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private LayerMask groundMask;

    private float movementSpeed;
    private bool isGrounded;
    private bool isRunning = false;
    private Vector2 moveDir;

    private void Awake()
    {
        // Create Player singleton instance
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    void Start()
    {
        // Get component refences
        playerRB = GetComponent<Rigidbody2D>();
        inputManager = InputManager.Instance;

        // Listen for events
        inputManager.OnJumpAction += InputManager_OnJumpAction;
        inputManager.OnRunAction += InputManager_OnRunAction;
    }

    private void FixedUpdate()
    {
        isGrounded = CheckForGround();

        HandleHorizontalPlayerMovement();
    }

    private bool CheckForGround()
    {
        float rayDist = 0.1f;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundMask);

        if (hit2D.collider != null)
        {
            // Debug.Log("Player is grounded");
            return true;
        }
        else
        {
            // Debug.Log("Player is not grounded");
            return false;
        }
    }

    private void HandleHorizontalPlayerMovement()
    {
        // Get horizontal movement input
        Vector2 inputVector = inputManager.GetHorizontalMovementVector();

        // Reset running if player stops moving horizontally
        if (inputVector.x == 0) isRunning = false;

        // Assign appropriate movement spped
        if (isRunning) movementSpeed = runningSpeed;
        else movementSpeed = walkingSpeed;
        // Debug.Log("Player movement spped: " + movementSpeed);

        // Calculate movement vector and assign it as rigidbody velocity
        moveDir = new Vector2(inputVector.x * movementSpeed, playerRB.velocity.y);
        playerRB.velocity = moveDir;
    }

    private void InputManager_OnJumpAction(object sender, System.EventArgs e)
    {
        if (isGrounded)
        {
            // Debug.Log("Jump Action!");
            playerRB.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void InputManager_OnRunAction(object sender, System.EventArgs e)
    {
        if (isGrounded)
        {
            // Debug.Log("Run Action!");
            isRunning = !isRunning;
        }
    }

    public bool GroundedCheck()
    {
        return isGrounded;
    }

    public float GetMovement()
    {
        return moveDir.x;
    }
}
