using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            PoolItem.Instance.ReturnObjePool(other.gameObject);
        }
    }
}
