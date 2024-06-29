using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireBall : MonoBehaviour
{
    public float bulletDamage;
    public float bulletSpeed = 2.5f;
    public float rotateSpeed = 150f;
    public float lifeTime = 4f; // Th?i gian t?n t?i c?a viên ??n tr??c khi tr? v? pool

    private Transform playerTransform;
    private Rigidbody2D bulletBody;
    private Coroutine returnToPoolCoroutine;

    void OnEnable()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        bulletBody = GetComponent<Rigidbody2D>();

        if (bulletBody == null)
        {
            bulletBody = gameObject.AddComponent<Rigidbody2D>();
            bulletBody.gravityScale = 0; // Ensure the bullet is not affected by gravity
        }

        returnToPoolCoroutine = StartCoroutine(ReturnToPoolAfterLifeTime());
    }

    void OnDisable()
    {
        if (returnToPoolCoroutine != null)
        {
            StopCoroutine(returnToPoolCoroutine);
        }
    }

    void FixedUpdate()
    {
        if (playerTransform == null)
            return;

        Vector2 direction = (Vector2)playerTransform.position - bulletBody.position;
        direction.Normalize();
        Debug.DrawLine(bulletBody.position, playerTransform.position, Color.red);

        float rotateAmount = Vector3.Cross(direction, transform.right).z;

        bulletBody.angularVelocity = -rotateAmount * rotateSpeed;
        bulletBody.velocity = transform.right * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.gameObject);
            PoolItem.Instance.ReturnObjePool(gameObject);
        }
    }

    private IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        PoolItem.Instance.ReturnObjePool(gameObject);
    }
}
