using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireBall : MonoBehaviour
{
    public float bulletDamage;
    public float bulletSpeed = 3f;
    public float rotateSpeed = 180f;
    public float lifeTime = 6f; // Th?i gian t?n t?i c?a viên ??n tr??c khi tr? v? pool

    private Transform playerTransform;
    private Rigidbody2D ri;
    private Coroutine returnToPoolCoroutine;

    void OnEnable()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ri = GetComponent<Rigidbody2D>();

        if (ri == null)
        {
            ri = gameObject.AddComponent<Rigidbody2D>();
            ri.gravityScale = 0; // Ensure the bullet is not affected by gravity
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

        Vector2 direction = (Vector2)playerTransform.position - ri.position;
        direction.Normalize();
        Debug.DrawLine(ri.position, playerTransform.position, Color.red);

        float rotateAmount = Vector3.Cross(direction, transform.right).z;

        ri.angularVelocity = -rotateAmount * rotateSpeed;
        ri.velocity = transform.right * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PoolItem.Instance.ReturnObjePool(gameObject);
            PlayerController.Instance.DamageReciever(2);
        }
    }

    private IEnumerator ReturnToPoolAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        PoolItem.Instance.ReturnObjePool(gameObject);
    }
}
