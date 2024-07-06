using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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