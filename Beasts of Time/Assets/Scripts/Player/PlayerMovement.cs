using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float walkingSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;

    private float DirX, DirY;
    private float rayDist;
    private bool jumpInput;
    private int groundMask;
    private bool isGrounded;

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
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the ground layer index
        groundMask = 1 << LayerMask.NameToLayer("Ground");

        // Determine the ray distance for ground detection using sprite size
        rayDist = (spriteRenderer.bounds.size.y / 2) + 0.1f;
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
            Debug.Log("Player is grounded");
        }
        else
        {
            isGrounded = false;
            Debug.Log("Player is not grounded");
        }


        // Update player movement from inputs
        playerRB.velocity = new Vector2(DirX * walkingSpeed, playerRB.velocity.y);

        // Check for jump
        if(jumpInput && isGrounded)
        {
            playerRB.AddForce(new Vector2(0, jumpForce));
            jumpInput = false;
        }
    }
}
