using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : BaseButton
{
    // Start is called before the first frame update
    public UIController ui;
    private bool isPaused = false;
    void Start()
    {
        ui = FindObjectOfType<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            system
            ui.btGameOver.SetActive(false);
            ui.scoreCoin.SetActive(false);
            ui.healthBar.SetActive(false);
            ui.pauseGame.SetActive(true);
        }
    }
}
