using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    private int addHealt = 3;
    public void Start()
    {
       StartCoroutine(WaitReturnPoolObject());
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.healthPlayer += addHealt;
            Debug.Log(PlayerController.Instance.healthPlayer);
            PoolItem.Instance.ReturnObjePool(gameObject);   
        }
    }
    private IEnumerator WaitReturnPoolObject()
    {
        yield return new WaitForSeconds(4f);
        PoolItem.Instance.ReturnObjePool(gameObject);
    }    
}
