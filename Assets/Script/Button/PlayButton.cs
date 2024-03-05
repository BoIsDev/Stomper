using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : BaseButton
{
    // Đặt tên scene mới ở đây
    public string newSceneName = "level 0";

    protected override void OnClick()
    {
        // Tải lại scene hiện tại
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(newSceneName);
    }
}
