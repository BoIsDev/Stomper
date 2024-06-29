using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject bullet;
    public float timeBxetweenShots = 2f;
    public float lastShotTime = 0f;
    public Transform pointBullte;

    // Update is called once per frame
    void Update()
    {
        timeBxetweenShots -= Time.deltaTime;
        if(timeBxetweenShots >=0) return;
        timeBxetweenShots = 2f;
        AutoShoot();
    }



    void AutoShoot()
    {
        //GameObject newBullet = Instantiate(bullet, pointBullte.position, Quaternion.identity);
        // thay doi huong
        PoolItem.Instance.GetObjItem(bullet,pointBullte).GetComponent<Rigidbody2D>().velocity = transform.up * 2; // Sử dụng -transform.right để bắn về phía bên trái
        //newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * 2;

        //StartCoroutine(DestroyBulletAfterDelay(GetObjItem(bullet,pointBullte), 2f));
    }
    //private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    Destroy(bullet);
    //    isShooting = false; // Set back to false after the bullet has been destroyed
    //    lastShotTime = 0f; // Đặt lại thời gian bắn cuối cùng
    //}



}
