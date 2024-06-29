using System.Collections;
using UnityEngine;

public class FireBallSkill : IState
{
    private BossController bossController;
    private Animator ani;
    private Coroutine waitingFireBallSkill;

    public FireBallSkill(BossController bossController)
    {
        this.bossController = bossController;
        this.ani = bossController.GetComponent<Animator>();
    }

    public void Enter()
    {
        ani.SetBool("isAttacking", true);
        waitingFireBallSkill = bossController.StartCoroutine(SpawnBulletFireAfterDelay());
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        ani.SetBool("isAttacking", false);
        ani.SetBool("isIdleing", true);
        if (waitingFireBallSkill != null)
        {
            bossController.StopCoroutine(waitingFireBallSkill);
            waitingFireBallSkill = null;
        }
    }

    public void Shoot()
    {
        GameObject bullet = PoolItem.Instance.GetObjItem(bossController.bulletFireBall, bossController.fireBallPos);
        bossController.StartCoroutine(WaitReturnPool(bullet));
    }

    private IEnumerator WaitReturnPool(GameObject bullet)
    {
        yield return new WaitForSeconds(4f);
        PoolItem.Instance.ReturnObjePool(bullet);
    }

    private IEnumerator SpawnBulletFireAfterDelay()
    {
        for (int i = 0; i < 4; i++)
        {
            Shoot();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
