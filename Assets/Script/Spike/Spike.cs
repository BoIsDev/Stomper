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
        
        PoolItem.Instance.GetObjItem(bullet,pointBullte).GetComponent<Rigidbody2D>().velocity = transform.up * 2; // Sử dụng -transform.right để bắn về phía bên trái
     
    }
    



}
