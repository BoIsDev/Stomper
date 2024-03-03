using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : BaseButton
{
   // Đặt tên scene mới ở đây

    public string newSceneName = "StartScene";

    protected override void OnClick()
    {
        // Tải lại scene hiện tại
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(newSceneName);
    }
}
