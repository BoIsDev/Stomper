using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPlayer : IState
{
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Animator ani;
    public float duckForce = 5f;
    public DuckPlayer(PlayerController playerController)
    {
        this.playerController = playerController;
        rb = playerController.GetComponent<Rigidbody2D>();
        ani = playerController.GetComponent<Animator>();

    }
    public void Enter()
    {
        ani.SetBool("isDuck", true);
        playerController.PlayerCollider.enabled = false;
        Duck();

    }
    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.S) && !playerController.isGrounded)
        {
            ani.SetBool("isDuck", true);

            duckForce *= 2f;
            Duck();
        }
    }

    public void Exit()
    {
        ani.SetBool("isDuck", false);
        playerController.PlayerCollider.enabled = true;
    }
    public void Duck()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.duck);
        rb.velocity = new Vector2(rb.velocity.x, -duckForce);
    }
}
