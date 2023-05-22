using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player player;
    private SpriteRenderer rend;
    private Animator animator;

    private float running;

    // Constants
    private const string IS_RUNNING = "IsRunning";
    private const string JUMP = "JumpTrigger";
    private const string IS_GROUNDED = "IsGrounded";

    private void Start()
    {
        // Get referenced components
        player = Player.Instance;
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Listen for jump event
        InputManager.Instance.OnJumpAction += InputManager_OnJumpAction;
    }

    private void Update()
    {
        FlipSprite();

        // Temporary control of animation
        animator.SetBool(IS_GROUNDED, player.GroundedCheck() );

        running = player.GetMovement();
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
