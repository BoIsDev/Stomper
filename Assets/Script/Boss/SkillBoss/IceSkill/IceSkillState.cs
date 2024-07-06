using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkillState : IState
{
    private BossController bossController;
    private Animator ani;
    private bool isShooting = false;
    private Coroutine shootingCoroutine;

    public IceSkillState(BossController bossController)
    {
        this.bossController = bossController;
        this.ani = bossController.GetComponent<Animator>();
    }
    public void Enter()
    {
        ani.SetBool("isAttacking", true);
        if (!isShooting)
        {
            shootingCoroutine = bossController.StartCoroutine(SetUpShootIce());
        }
    }
    public void Execute()
    {
    }
    public void Exit()
    {
        ani.SetBool("isIdleing", true);
        ani.SetBool("isAttacking", false);
        if (shootingCoroutine != null)
        {
            bossController.StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
            isShooting = false;
        }
        bossController.isStateFree = true;
    }

    public void Shoot()
    {
        foreach (Transform pointIce in bossController.lstPointIce)
        {
            GameObject icePrefabs = PoolItem.Instance.GetObjItem(bossController.BulletIce, pointIce);
            Vector3 direction = bossController.Player.position - icePrefabs.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            icePrefabs.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            icePrefabs.GetComponent<IceBullet>();
            bossController.StartCoroutine(ShootAfterDelay(icePrefabs));
        }
    }
    private IEnumerator ShootAfterDelay(GameObject icePrefabs)
    {
        
        Vector2 direction = (bossController.Player.position - icePrefabs.transform.position).normalized;
        yield return new WaitForSeconds(1f);
        icePrefabs.GetComponent <Rigidbody2D>().velocity = direction * 20;

    }
    private IEnumerator SetUpShootIce()
    {
        for (int i = 0; i < 4; i++)
        {
            Shoot();
            yield return new WaitForSeconds(0.25f);
        }
        BossController.Instance.count++;
        Debug.Log("Count: " + BossController.Instance.count);
        isShooting = true;
    }
}
