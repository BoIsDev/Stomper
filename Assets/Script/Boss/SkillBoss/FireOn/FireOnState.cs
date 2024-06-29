using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireOnState : IState
{
    private BossController bossController;
    private Animator ani;
    private bool isShooting = false;
    private Coroutine waitCollider;
    private Transform posFireOn;
    public FireOnState(BossController bossController)
    {
        this.bossController = bossController;
        this.ani = bossController.GetComponent<Animator>();
    }
    public void Enter()
    {
        posFireOn = bossController.player;
        ani.SetBool("isAttacking", true);
       waitCollider = bossController.StartCoroutine(SetUpShootIce());
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        if(waitCollider != null)
        {
            bossController.StopCoroutine(waitCollider);
            waitCollider = null;
        }
    }
    public void ShootFireOn()
    {
        GameObject newFireOn = PoolItem.Instance.GetObjItem(bossController.bulletFireOn, bossController.fireOnPos);
        newFireOn.transform.position = bossController.fireOnPos.position;
        bossController.StartCoroutine(WaitReturnPool(newFireOn));
    }

    private IEnumerator WaitReturnPool(GameObject newFireOn)
   {
        yield return new WaitForSeconds(1f);
        bossController.isStateFree = true;
        PoolItem.Instance.ReturnObjePool(newFireOn);
   }
    private IEnumerator SetUpShootIce()
    {
        for (int i = 0; i < 4; i++)
        {
            ShootFireOn();
            yield return new WaitForSeconds(0.5f);
        }
        isShooting = true;
        BossController.Instance.count++;
    }
}
