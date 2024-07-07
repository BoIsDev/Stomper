using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrap : MonoBehaviour
{
    [SerializeField]private GameObject bulletPlant;
    [SerializeField]private Transform shootPoint;
    private float shootRate = 0.2f;
    private float nextShootTime = 0f;
    private float shootForce = 8f;  // Lực bắn ban đầu
    private float shootAngle = 135f;  // Góc bắn (độ)
    private float shootAngle2 = 110f;
    private float countShoot = 0;
    private float shootInterval = 3f;
    private bool isChangeAngle = false;
    Coroutine cor;
    void Start()
    {
        cor = StartCoroutine(SpawnBulletAfterDelay());
    }
    void ShotTimetime()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            countShoot++;
            if (countShoot >= 3)
            {
                countShoot = 0;
                nextShootTime = Time.time + shootInterval;
                if (isChangeAngle)
                {
                    shootAngle = shootAngle2;
                    isChangeAngle = !isChangeAngle;
                }
                else
                {
                    shootAngle = 135f;
                    isChangeAngle = !isChangeAngle;
                }
            }
            else
            {
                nextShootTime = Time.time + shootRate;
            }
        }
    }
    void Shoot()
    {
        GameObject newBullet = PoolItem.Instance.GetObjItem(bulletPlant, shootPoint);
        newBullet.SetActive(true);
        AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.plantShoot);
        newBullet.name = bulletPlant.name;
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        // Chuyển đổi góc bắn từ độ sang radian
        float angleInRadians = shootAngle * Mathf.Deg2Rad;
        // Tính toán vector lực theo góc bắn
        Vector2 force = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) * shootForce;
        // Áp dụng lực ban đầu cho đạn để tạo chuyển động parabol
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    private IEnumerator SpawnBulletAfterDelay()
    {
        while (true)
        {
            if (isChangeAngle)
            {
                shootAngle = shootAngle2;
                isChangeAngle = !isChangeAngle;
            }
            else
            {
                shootAngle = 135f;
                isChangeAngle = !isChangeAngle;
            }
            for (int i = 0; i < 3; i++)
            {
                Shoot();
                yield return new WaitForSeconds(shootRate);
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }

}
