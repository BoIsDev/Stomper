using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurikenSkill : IState
{
    private BossController bossController;
    private Animator ani;
    public float shootAngle = 115f;  // Góc bắn (độ)
    public float shootForce = 9.5f;  // Lực bắn ban đầu
    public float[] arrAngle = { 115f,120f,120f };

    //private Coroutine waitingFireBallSkill;

    public SurikenSkill(BossController bossController)
    {
        this.bossController = bossController;
        this.ani = bossController.GetComponent<Animator>();

    }
    public void Enter()
    {
        ani.SetBool("isAttacking", true);
        ShootSuriken();
    }

    public void Execute()
    {
    }
    public void Exit()
    {
        ani.SetBool("isAttacking", false);
        ani.SetBool("isIdleing", true);
    }


    private void ShootSuriken()
    {
        GameObject surikenPrefabs = PoolItem.Instance.GetObjItem(bossController.BulletSuriken, bossController.SurikenHold);
        Rigidbody2D rb = surikenPrefabs.GetComponent<Rigidbody2D>();
        float angleInRadians = shootAngle * Mathf.Deg2Rad;
        Vector2 force = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * shootForce;
        // Áp dụng lực ban đầu cho đạn để tạo chuyển động parabol
        rb.AddForce(force, ForceMode2D.Impulse);
        bossController.StartCoroutine(ReturnPoolSuriken(surikenPrefabs));
    }


    private IEnumerator ReturnPoolSuriken(GameObject surikenPrefabs)
    {
        yield return new WaitForSeconds(1.3f);
        float shootAngle = 60f;
        AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.attackSurikenBig);
        for (int i=0; i<6; i++)
        {
            GameObject surikenSpawn = PoolItem.Instance.GetObjItem(bossController.BulletSuriken, bossController.PointSuriken);
            Rigidbody2D rb = surikenSpawn.GetComponent<Rigidbody2D>();
            surikenSpawn.transform.localScale = new Vector3(0.5f, 0.5f,1f);
            float angleInRadians = shootAngle * Mathf.Deg2Rad;
            Vector2 force = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * 6;
            rb.AddForce(force, ForceMode2D.Impulse);
            shootAngle += 20f;
        }
        PoolItem.Instance.ReturnObjePool(surikenPrefabs);
    }
}
