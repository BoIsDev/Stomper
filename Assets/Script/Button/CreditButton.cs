using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditButton : BaseButton
{
    // Start is called before the first frame update
  public string newSceneName = "Credit";
    
   
    protected override void OnClick()
    {
        // Tải lại scene hiện tại
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(newSceneName);
    }
}
