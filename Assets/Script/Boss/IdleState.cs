using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private BossController bossController;
    public IdleState(BossController bossController)
    {
        this.bossController = bossController;
    }

    public void Enter()
    {  
        bossController.GetComponent<Animator>().SetBool("isIdleing", true);
        Debug.Log("IdleEnter");
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {
        Debug.Log("IdleExit");
        bossController.GetComponent<Animator>().SetBool("isIdleing", false);

    }
}

