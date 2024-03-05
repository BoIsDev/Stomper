using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetBT : BaseButton
{
    // Start is called before the first frame update
    protected override void OnClick()
    {
        // Tải lại scene hiện tại
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}