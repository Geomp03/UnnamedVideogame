using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private PlayerMovement movement;
    private SpriteRenderer rend;
    private Animator animator;

    private float running;

    // Constants
    private const string IS_RUNNING = "IsRunning";
    private const string JUMP = "JumpTrigger";
    private const string IS_GROUNDED = "IsGrounded";

    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Listen for jump event
        inputManager.OnJumpAction += InputManager_OnJumpAction;
    }

    private void Update()
    {
        FlipSprite();

        // Temporary control of animation
        animator.SetBool(IS_GROUNDED, movement.isGrounded);

        running = inputManager.GetHorizontalMovementVector().x;
        animator.SetBool(IS_RUNNING, running != 0);
    }

    private void InputManager_OnJumpAction(object sender, System.EventArgs e)
    {
        animator.SetTrigger(JUMP);
    }

    private void FlipSprite()
    {
        if (running > 0)
            rend.flipX = false;
        else if(running < 0)
            rend.flipX = true;
    }
}
