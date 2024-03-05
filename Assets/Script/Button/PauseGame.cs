using UnityEngine;

public class PauseButton : BaseButton
{
    public GameObject PauseGame; // Gán từ Inspector

    public AudioSource musicSource; // Gán từ Inspector


    protected override void OnClick()
    {
        PauseGame.SetActive(false);
        Time.timeScale = 1f;
        musicSource.UnpauseMusic();
    }
}
