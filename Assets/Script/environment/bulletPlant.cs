using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletPlant : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.DamageReciever(1);
            PoolItem.Instance.ReturnObjePool(gameObject);
        }
    }
}
