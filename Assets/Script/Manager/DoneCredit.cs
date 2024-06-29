using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoneCredit : MonoBehaviour
{
    public float delayBeforeEndCredit = 15f;
    public string newSceneName = "StartScene";

    void Start()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    IEnumerator WaitAndLoadScene()
    {
        // Chờ đợi một khoảng thời gian trước khi chuyển màn hình
        yield return new WaitForSeconds(delayBeforeEndCredit);

        // Chuyển màn hình sau khi kết thúc animation
        SceneManager.LoadScene(newSceneName);
    }
}
