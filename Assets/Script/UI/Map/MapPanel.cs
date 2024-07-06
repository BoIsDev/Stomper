using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPanel : MonoBehaviour
{
    public Button btnPrePage;
    public Button btnNextPage;
    public Text txtNumberPage;
    public Transform holdMapLevel;
    public Button btnUnLockLevel;
    public Button btnLockLevel;
    public Button btnBoos;
    public int maxMap = 17;
    public int initNumberPage = 1;
    int currentMap = 1;
    bool isBoss = false;

    private static MapPanel instance;
    public static MapPanel Instance => instance;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        CreateMap();
        OnclickButton();
    }


    public void CreateMap()
    {
        for (int i = 1; i <= maxMap; i++)
        {
            int openLevel = i;
            if (i <= currentMap)
            {
                Button btnMapLevel = Instantiate(btnUnLockLevel, holdMapLevel);
                btnMapLevel.name = i.ToString();
                btnMapLevel.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                btnMapLevel.onClick.AddListener(() => OnClick(btnMapLevel.name));
            }
            else
            {
                Button btnMapLevel = Instantiate(btnLockLevel, holdMapLevel);
                btnMapLevel.name = i.ToString();
                btnMapLevel.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            }
        }
        Button btnBoss = Instantiate(btnBoos, holdMapLevel);
        btnBoss.name = "Boss";
        btnBoss.transform.GetChild(1).GetComponent<Text>().text = "Boss";
        if (isBoss)
        {
            btnBoss.onClick.AddListener(() => OnClick(btnBoss.name));

        }
    }

    public void Page(int numberPage)
    {
        initNumberPage += numberPage;
        int maxPage;
        if (maxMap % 9 == 0)
        {
            maxPage = maxMap / 9;
        }
        else
        {
            maxPage = (maxMap / 9) + 1;
        }
        if (initNumberPage <= 0)
        {
            initNumberPage = maxPage;
            txtNumberPage.text = "" + initNumberPage;
            PageManager.Instance.SetPage(initNumberPage);
        }
        else if (initNumberPage > maxPage)
        {
            initNumberPage = 1;
            txtNumberPage.text = "" + initNumberPage;
            PageManager.Instance.SetPage(initNumberPage);
        }
        else
        {
            txtNumberPage.text = "" + initNumberPage;
            PageManager.Instance.SetPage(initNumberPage);
        }
    }
    protected virtual void OnClick(string newSceneName)
    {

        SceneManager.LoadScene(newSceneName);
    }
    public void OnclickButton()
    {
        btnPrePage.onClick.AddListener(() => Page(-1));
        btnNextPage.onClick.AddListener(() => Page(1));
    }
}
