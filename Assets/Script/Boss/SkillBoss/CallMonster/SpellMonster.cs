using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMonster : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Instance.DamageReciever(2);
            if(gameObject.activeSelf)
            {
                StartCoroutine(ReturPool());
            }
        }
    }

    private IEnumerator ReturPool()
    {
        yield return new WaitForSeconds(2f);
        PoolItem.Instance.ReturnObjePool(gameObject);
    }
}
