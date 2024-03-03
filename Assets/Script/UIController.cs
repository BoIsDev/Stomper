using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject btGameOver = null;
    
    public GameObject scoreCoin = null;

    public GameObject healthBar = null;

    public GameObject btPlayGame = null;

    public GameObject pauseGame = null;


    private void Awake()
    {
        btGameOver.SetActive(false);
        scoreCoin.SetActive(true);
        healthBar.SetActive(true);
        pauseGame.SetActive(false);
    }

  
}
