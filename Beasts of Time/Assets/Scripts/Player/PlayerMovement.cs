using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;

    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;

    public float DirX;
    private Vector2 moveDir;
    public bool jumpInput;
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
    }

    // Update is called once per frame
    void Update()
    {
        // Check for horizontal movement inputs
        DirX = Input.GetAxisRaw("Horizontal");

        // Check if jump key is pressed - spacebar
        if (Input.GetButtonDown("Jump"))
            jumpInput = true;
    }

    // Physics based time step
    private void FixedUpdate()
    {
        // Check if the player is grounded
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundMask);

        if (hit2D.collider != null)
        {
            isGrounded = true;
            // Debug.Log("Player is grounded");
        }
        else
        {
            isGrounded = false;
            // Debug.Log("Player is not grounded");
        }

        // Update player movement from inputs
        moveDir = new Vector2(DirX * walkingSpeed, playerRB.velocity.y);
        playerRB.velocity = moveDir;

        // Check for jump
        if (jumpInput && isGrounded)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
            jumpInput = false;
        }
    }
}
