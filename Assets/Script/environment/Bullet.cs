using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    private PlayerController pl;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = FindObjectOfType<PlayerController>();
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") )
        {
            if (pl.healthPlayer > 0)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.hit);
                pl.DamageReciever(1);
            }
        }
        if (collider.gameObject.CompareTag("Ground") || collider.gameObject.CompareTag("Player"))
        {
            PoolItem.Instance.ReturnObjePool(gameObject);
        }
    }

   
}
