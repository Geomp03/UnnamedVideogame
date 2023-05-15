using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement movement;
    private SpriteRenderer rend;
    private Animator animator;

    private const string IS_RUNNING = "IsRunning";

    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FlipSprite();
        animator.SetBool(IS_RUNNING, movement.isGrounded && movement.DirX != 0);
    }

    private void FlipSprite()
    {
        if (movement.DirX > 0)
        {
            rend.flipX = false;
        }
        else if(movement.DirX < 0)
        {
            rend.flipX = true;
        }
    }
}
