using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private LayerMask groundMask;

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

        // Listen for events
        inputManager.OnJumpAction += InputManager_OnJumpAction;
    }

    // Physics based time step
    private void FixedUpdate()
    {
        isGrounded = CheckForGround();
        HandleMovement();
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

    private void HandleMovement()
    {
        Vector2 inputVector = inputManager.GetHorizontalMovementVector();

        Vector2 moveDir = new Vector2(inputVector.x * walkingSpeed, playerRB.velocity.y);
        playerRB.velocity = moveDir;
    }
}
