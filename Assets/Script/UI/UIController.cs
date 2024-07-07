
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    [SerializeField] private Text txtTimeBoss;
    [SerializeField] private Text txtCoint;
    [SerializeField] private GameObject resumeButton;
    public int count = 0;
    public float startTime;
    private bool isCoin = false;
    private int countMax = 5;
    private float timeVictory = 59f;
    public static UIController Instance => instance;
    private static UIController instance;
    private PlayerManager controls;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        controls = new PlayerManager();
        controls.Enable();
        controls.Stomper.Pause.performed += ctx =>
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        };
    }
    public void Start()
    {
        panelPause.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "boss")
        {
            txtTimeBoss.gameObject.SetActive(true);
            isCoin = false;
        }
        else
        {
            txtTimeBoss.gameObject.SetActive(false);
            txtTimeBoss = null;
            isCoin = true;
        }
        startTime = Time.time;

    }
    public void Update()
    {
        if (isCoin == true && txtCoint != null)
        {
            txtCoint.text = count.ToString() + "/" + countMax.ToString();
        }
        else
        {
            TimeVictory();
            txtCoint = null;
        }
        if (panelPause.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void TimeVictory()
    {
        float timeElapsed = Time.time - startTime;
        float timeRemaining = timeVictory - timeElapsed;
        int roundedTime = Mathf.FloorToInt(timeRemaining);
        string formattedTime = roundedTime.ToString();
        if (roundedTime >= 10)
        {
            txtTimeBoss.text = "0:" + "" + formattedTime;
        }
        else
        {
            txtTimeBoss.text = "0:" + "" + "0" + formattedTime;

        }
        if (timeRemaining <= 0)
        {
            txtTimeBoss.text = "0:00";
            string newSceneName = "Victory";
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(newSceneName);
        }
    }
    public void GameOver()
    {
        resumeButton.SetActive(false);
        panelPause.SetActive(true);
    }
    public void OnEnable()
    {
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }
}
