using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPlayer : IState
{
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Animator ani;
    private Enemy enemy;
    private float rollSpeed = 8f;
    private float rollDuration = 0.5f;
    private float rollTimer;
    private int dameRoll = 4;
    public RollPlayer(PlayerController playerController)
    {
        this.playerController = playerController;
        rb = playerController.GetComponent<Rigidbody2D>();
        ani = playerController.GetComponent<Animator>();
    }

    public void Enter()
    {
        ani.SetBool("isRoll", true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.acttack);
    }

    public void Execute()
    {
        rb.velocity = new Vector2(playerController.xDirection * rollSpeed, rb.velocity.y);
    }

    public void Exit()
    {
        ani.SetBool("isRoll", false);
        rb.gravityScale = 1;

    }
    public void CollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        Debug.Log("RollCheckPlayer");
        if (enemy != null)
        {
            enemy.DamageReciever(dameRoll);
            Debug.Log("RollPlayer");
        }
    }
}
