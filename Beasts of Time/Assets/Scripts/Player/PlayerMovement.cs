using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;

    private Vector2 moveDir;
    private float rayDist = 0.1f;
    private int groundMask;
    public bool isGrounded;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogget.logEnabled = false;
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        // Component refences
        playerRB = GetComponent<Rigidbody2D>();

        // Get the ground layer index
        groundMask = 1 << LayerMask.NameToLayer("Ground");

        // Listen for jump event
        inputManager.OnJumpAction += InputManager_OnJumpAction;
    }

    // Physics based time step
    private void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = CheckForGround();

        // Update player movement from inputs
        Vector2 inputVector = inputManager.GetHorizontalMovementVector();

        moveDir =  new Vector2(inputVector.x * walkingSpeed, playerRB.velocity.y);
        playerRB.velocity = moveDir;
    }

    private void InputManager_OnJumpAction(object sender, System.EventArgs e)
    {
        if (isGrounded)
            playerRB.AddForce(new Vector2(0, jumpForce));
    }

    private bool CheckForGround()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundMask);

        if (hit2D.collider != null)
        {
            return true;
            // Debug.Log("Player is grounded");
        }
        else
        {
            return false;
            // Debug.Log("Player is not grounded");
        }
    }
}
