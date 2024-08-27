using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TigerForge;

public class Door : MonoBehaviour
{
    private float delayBeforeLoading = 1.5f;
    [SerializeField]private string newSceneName = "boss";
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (UIController.Instance.count == 5)
            {
                MapPanel.Instance.currentMap++;
                EasyFileSave myFile = new EasyFileSave();
                myFile.Add("initMap", MapPanel.Instance.currentMap);
                myFile.Save();
                MapPanel.Instance.SaveLevel(MapPanel.Instance.currentMap);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.door);
                col.gameObject.SetActive(false);
                LoadScreen();
            }
        }
    }
    public void LoadScreen()
    {
        StartCoroutine(LoadAfterDelay());
        UIController.Instance.count = 0;
    }
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        SceneManager.LoadScene(newSceneName);
    }
}