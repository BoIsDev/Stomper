using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    Player pl;
    Rigidbody2D rb;
    AudioManager audio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pl = FindObjectOfType<Player>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

  
    }

     private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") )
        {
            if (pl.healthPlayer > 0)
            {
                audio.PlaySFX(audio.hit);

                pl.healthPlayer--;
            }
        }
        
        if (collider.gameObject.CompareTag("Ground") || collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

   
}
