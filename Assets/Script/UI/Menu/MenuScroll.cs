using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    public Button btnPlay;
    public Button btnMap;
    public Button btnCredit;
    public GameObject MapPanel;
    [SerializeField] private Button btnBack;


    public void Start()
    {
        MapPanel.SetActive(false);

        btnCredit.onClick.AddListener(OpenCredit);
        btnPlay.onClick.AddListener(OpenPlay);
        btnMap.onClick.AddListener(OpenMap);
        btnBack.onClick.AddListener(OnBack);

    }
    public void OpenMap()
    {
        MapPanel.SetActive(true);
        
    }    
    public void OpenPlay()
    {
        string newSceneName = "1";
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(newSceneName);
    }
    public void OpenCredit()
    {
        string newSceneName = "btnCredit";
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(newSceneName);
    }
    public void OnBack()
    {
        MapPanel.SetActive(false);
    }
}
