using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMonsterState : IState
{
    private BossController bossController;
    private Animator ani;
    private Coroutine callMonsterCoroutine;

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
    }

    public void Exit()
    {
        ani.SetBool("isAttacking", false);
        ani.SetBool("isIdleing", true);

        if (callMonsterCoroutine != null)
        {
            bossController.StopCoroutine(callMonsterCoroutine);
        }
    }

    private void CallMonster()
    {
        foreach (Transform pos in bossController.lstPointMonster)
        {
            GameObject monster = PoolItem.Instance.GetObjItem(bossController.Monster, pos);
            bossController.StartCoroutine(ReturnPoolMonster(monster));
        }
    }

    private IEnumerator ReturnPoolMonster(GameObject monster)
    {
        yield return new WaitForSeconds(8f);
        PoolItem.Instance.ReturnObjePool(monster);
    }

    public void OnTriggetEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ObseverManager.Instance.DamageReciever(1);
        }
    }
}
