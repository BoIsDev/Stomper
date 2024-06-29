using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMonsterState : IState
{
    private BossController bossController;
    private Animator ani;

    public CallMonsterState(BossController bossController)
    {
        this.bossController = bossController;
        this.ani = bossController.GetComponent<Animator>();
    }

    public void Enter()
    {
        ani.SetBool("isAttacking", true);
        CallMonster();
        Debug.Log("Call Monster");
    }

    public void Execute()
    {
        // No actions needed during Execute phase for calling monsters
    }

    public void Exit()
    {
        ani.SetBool("isAttacking", false);
        ani.SetBool("isIdleing", true);
    }

    private void CallMonster()
    {
        foreach (Transform pos in bossController.lstPointMonster)
        {
            GameObject monster = PoolItem.Instance.GetObjItem(bossController.monster, pos);
            // Optionally, you can add more setup or initialization for each monster here
        }
    }
}
