using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCoroutine : MonoBehaviour
{
    public GameObject bullet;
    public Transform pointBullte;
    private bool canShoot = true;

    private void Update()
    {
        if(canShoot)
        {
            canShoot = false;
            StartCoroutine(SpawnBulletAfterDelay(bullet, 2f));
        }
    }
    
    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        //PoolItem.Instance.ReturnObjePool(bullet);
    }
    
    private IEnumerator SpawnBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
        GameObject newBullet = PoolItem.Instance.GetObjItem(bullet, pointBullte);
        newBullet.transform.position = pointBullte.position; // Set bullet's position
        newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * 2; // Set bullet's velocity
        StartCoroutine(DestroyBulletAfterDelay(newBullet, 3f));
    }
}
