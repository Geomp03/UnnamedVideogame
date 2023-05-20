using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ITimeBubble
{
    private Rigidbody2D playerRB;
    private InputManager inputManager;

    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private LayerMask groundMask;

    private bool isGrounded;
    private Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        // Component refences
        playerRB = GetComponent<Rigidbody2D>();
        inputManager = InputManager.Instance;

        // Listen for events
        inputManager.OnJumpAction += InputManager_OnJumpAction;
    }

    // Physics based time step
    private void FixedUpdate()
    {
        // Check if player is Grounded
        isGrounded = CheckForGround();

        // Handle Player movement
        Vector2 inputVector = inputManager.GetHorizontalMovementVector();

        moveDir = new Vector2(inputVector.x * walkingSpeed, playerRB.velocity.y);
        playerRB.velocity = moveDir;
    }

    private void InputManager_OnJumpAction(object sender, System.EventArgs e)
    {
        if (isGrounded)
            playerRB.AddForce(new Vector2(0, jumpForce));
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

    public void SlowDownTimePerception()
    {
        Debug.Log("Slow down time perception for player");
    }
    public void SpeedUpTimePerception()
    {
        Debug.Log("Speed up time perception for player");
    }
    public void ResetTimePerception()
    {
        Debug.Log("Reset time perception for player");
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
