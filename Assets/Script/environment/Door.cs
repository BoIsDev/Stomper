using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float delayBeforeLoading = 1.5f; 
    public string newSceneName = "Level 1"; 
    public CoinManager cm;
    AudioManager audio;
    

    void Start(){
        cm = FindObjectOfType<CoinManager>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
   private void OnTriggerEnter2D(Collider2D col)
   {
       if(col.CompareTag("Player"))
       {
            if(cm.coinCount == 5)
            {
            audio.PlaySFX(audio.door);
            col.gameObject.SetActive(false);
            LoadScreen();
            }
           
       }
   }

    public void LoadScreen()
    {
        this.cm.levelCount++;
        StartCoroutine(LoadAfterDelay());
        cm.coinCount =0;
        Debug.Log(cm.levelCount);
    }

    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        SceneManager.LoadScene(newSceneName);
    }
}
