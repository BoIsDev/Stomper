using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMonsterState : IState
{
    private MosterController mosterController;
    private Animator ani;
    public IdleMonsterState(MosterController mosterController)
    {
        this.mosterController = mosterController;
        ani = mosterController.GetComponent<Animator>();
    }

    public void Enter()
    {
        ani.SetBool("isIdleing", true);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
