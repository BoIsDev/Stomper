using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPlayer : IState
{
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Animator ani;
    public float moveSpeed = 4f;

    public WalkPlayer(PlayerController playerController)
    {
        this.playerController = playerController;
        rb = playerController.GetComponent<Rigidbody2D>();
        ani = playerController.GetComponent<Animator>();
    }



    public void Enter()
    {
        if (playerController.isGrounded)
        {
            ani.SetBool("isWalk", true);
            ani.SetBool("isJump", false);
        }
    }

    public void Execute()
    {
        rb.velocity = new Vector2(playerController.xDirection * moveSpeed, rb.velocity.y);
        Debug.Log(playerController.xDirection);
        if (playerController.xDirection > 0 && !playerController.isFacingRight || playerController.xDirection < 0 && playerController.isFacingRight)
        {
            playerController.FlipObj();
        }
    }

    public void Exit()
    {
        ani.SetBool("isWalk", false);
    }
}
