using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayer : IState
{
    private PlayerController playerController;
    public IdlePlayer(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    public void Enter()
    {
        playerController.GetComponent<Animator>().SetBool("isIdle", true);
    }
    public void Execute()
    {
    }
    public void Exit()
    {
        playerController.GetComponent<Animator>().SetBool("isIdle", false);
    }
}

