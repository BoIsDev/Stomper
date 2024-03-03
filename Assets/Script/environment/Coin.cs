using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    private CoinManager cm;
        AudioManager audio;

    void Start()
    {
        cm = FindObjectOfType<CoinManager>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

      private void OnTriggerEnter2D(Collider2D collision)
    {
    if(collision.gameObject.CompareTag("Player"))
        {
            audio.PlaySFX(audio.pick);
            cm.coinCount++;
            Destroy(gameObject);
        }
    }
}
