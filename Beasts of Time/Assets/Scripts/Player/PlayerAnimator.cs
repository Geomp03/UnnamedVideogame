using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement movement;
    private SpriteRenderer rend;
    private Animator animator;

    // Constants
    private const string IS_RUNNING = "IsRunning";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_GROUNDED = "IsGrounded";

    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FlipSprite();

        // Temporary control of animation
        animator.SetBool(IS_GROUNDED, movement.isGrounded);
        animator.SetBool(IS_JUMPING, movement.jumpInput);
        animator.SetBool(IS_RUNNING, movement.DirX != 0);
    }

    private void FlipSprite()
    {
        if (movement.DirX > 0)
            rend.flipX = false;
        else if(movement.DirX < 0)
            rend.flipX = true;
    }
}
