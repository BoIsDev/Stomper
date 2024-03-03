using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

     Player pl;
    // Start is called before the first frame update
    void Awake()
    {
        pl = FindObjectOfType<Player>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            pl.healthPlayer = 0;
        }
    }
}