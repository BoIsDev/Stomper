using UnityEngine;
using System.Collections;

public class IceBullet : MonoBehaviour
{
    public float bulletSpeed = 20f; // Tốc độ di chuyển của đạn
    public Transform player;
    private Rigidbody2D rb;
    public GameObject cashIce;
    private static IceBullet instance;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        cashIce.SetActive(true);
        StartCoroutine(SetActiceCashIce());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PoolItem.Instance.ReturnObjePool(gameObject);
            PlayerController.Instance.DamageReciever(1);
        }
        else if (other.CompareTag("Ground"))
        {
            PoolItem.Instance.ReturnObjePool(gameObject);
        }
    }
    private IEnumerator SetActiceCashIce()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.attackIceSkill);

        cashIce.SetActive(false);
    }
}