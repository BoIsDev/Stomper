using System.Collections;
using UnityEngine;

public class Spike_Left : MonoBehaviour
{
    public GameObject bullet;
    public float timeBxetweenShots = 2f;
    public float lastShotTime = 0f;
    private bool isShooting = false;
    Rigidbody2D rb;

    Bullet bl;

    public Transform pointBullte;

    // Start is called before the first frame update
    void Start()
    {
        bl = FindObjectOfType<Bullet>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Time.time - lastShotTime >= timeBxetweenShots)
        {
            lastShotTime = Time.time;
            isShooting = true;
            AutoShoot();
        }
    }
   void AutoShoot()
{
    GameObject newBullet = Instantiate(bullet, pointBullte.position, Quaternion.identity);
    // thay doi huong
    newBullet.GetComponent<Rigidbody2D>().velocity = -transform.right * 2; // Sử dụng -transform.right để bắn về phía bên trái

    StartCoroutine(DestroyBulletAfterDelay(newBullet, 2f));
}

     private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
        isShooting = false; // Set back to false after the bullet has been destroyed
        lastShotTime = 0f; // Đặt lại thời gian bắn cuối cùng
    }
}
