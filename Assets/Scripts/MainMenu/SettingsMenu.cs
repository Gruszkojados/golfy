using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    public GameObject tutorial;
    int loadCtl = 0;
    public void ShowHideMenu() {
        SoundsAction.ButtonClick();

        if(tutorial.activeSelf) {
            tutorial.SetActive(false);
            return;
        }

        if(gameObject.activeSelf) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
    public void LoadMiniGame() {
        loadCtl++;
        StartCoroutine(loadGameWait());
    }

    public IEnumerator loadGameWait() {
        if(loadCtl > 8) {
            SceneManager.LoadScene(2);
        }
        yield return new WaitForSeconds(5f);
        loadCtl = 0;
    }
}
