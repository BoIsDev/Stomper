using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireOnState : IState
{
    private BossController bossController;
    private Animator ani;
    private Coroutine waitCollider;
    private Transform posFireOn;
    public FireOnState(BossController bossController)
    {
        this.bossController = bossController;
        this.ani = bossController.GetComponent<Animator>();
    }
    public void Enter()
    {
        posFireOn = bossController.Player;
        ani.SetBool("isAttacking", true);
       waitCollider = bossController.StartCoroutine(SetUpShootFireOn());
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
        GameObject newFireOn = PoolItem.Instance.GetObjItem(bossController.BulletFireOn, bossController.FireOnPos);
        newFireOn.transform.position = bossController.FireOnPos.position;
        AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.attackFireOn);
        bossController.StartCoroutine(WaitReturnPool(newFireOn));
    }

    private IEnumerator WaitReturnPool(GameObject newFireOn)
   {
        yield return new WaitForSeconds(1f);
        bossController.isStateFree = true;
        PoolItem.Instance.ReturnObjePool(newFireOn);
   }
    private IEnumerator SetUpShootFireOn()
    {
        for (int i = 0; i < 4; i++)
        {
            ShootFireOn();
            yield return new WaitForSeconds(0.5f);
        }
        BossController.Instance.count++;
    }
}
