using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : IState
{
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Animator ani;
    public float jumpForce = 5f;
    public float wallSlideSpeed = 2f;
    public JumpPlayer(PlayerController playerController)
    {
        this.playerController = playerController;
        rb = playerController.GetComponent<Rigidbody2D>();
        ani = playerController.GetComponent<Animator>();
    }
    public void Enter()
    {
        ani.SetBool("isJump", true);
        Jump();
        playerController.jumpCount++;
    }
    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playerController.isGrounded && playerController.jumpCount < playerController.maxJumpCount)
        {
            ani.SetBool("isJump", true);
            jumpForce *= 0.8f;
            Jump();
            playerController.jumpCount++;
        }
    }
    public void Exit()
    {
        ani.SetBool("isJump", false);
    }
    public void Jump()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
