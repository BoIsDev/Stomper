using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelPauseScene : MonoBehaviour
{
    public Button btnResume;
    public Button btnNewGame;
    public Button btnSetting;
    public Button btnExit;
    public Button btnBack;
    public GameObject imgBoxSetting;

    public void Start()
    {
        imgBoxSetting.SetActive(false);
        btnResume.onClick.AddListener(OnResume);
        btnNewGame.onClick.AddListener(OnNewGame);
        btnSetting.onClick.AddListener(OnSetting);
        btnBack.onClick.AddListener(OnBack);
        btnExit.onClick.AddListener(OnExit);
    }
    public void OnResume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void OnNewGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Debug.Log("Timeaaa: " + Time.timeScale);
        UIController.Instance.startTime = Time.time;

        //if (BossController.Instance. != null)
        //{
        //    Debug.Log("Time: " + UIController.Instance.startTime);
        //}
    }
    public void OnSetting()
    {
        imgBoxSetting.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnBack()
    {
        imgBoxSetting.SetActive(false);
    }
}

