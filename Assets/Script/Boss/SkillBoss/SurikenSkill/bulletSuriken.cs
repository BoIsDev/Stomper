using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSuriken : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.DamageReciever(1);
            AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.attackSurikenSmall);
            PoolItem.Instance.ReturnObjePool(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            PoolItem.Instance.ReturnObjePool(gameObject);
        }
    }
}
