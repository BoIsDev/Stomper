using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject btPauseGame = null;
    
    public GameObject scoreCoin = null;

    public GameObject healthBar = null;


    public AudioManager audioManager; // Gán từ Inspector


    private void Awake()
    {
        btPauseGame.SetActive(false);
        scoreCoin.SetActive(true);
        healthBar.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            btPauseGame.SetActive(true);
            scoreCoin.SetActive(false);
            healthBar.SetActive(false);
            audioManager.PauseMusic();
        }
     
   
    }

    

  
}
