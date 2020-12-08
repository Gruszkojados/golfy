using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    public static event Action<String> OnHitButton = (text) => {};
    public GameObject tutorial;
    public GameObject clearProgress;

    int loadCtl = 0;
    public void ShowHideMenu() { // Show/hide settings GUI
        SoundsAction.ButtonClick();

        Transform tmp = transform.GetChild(0);
        TextMeshProUGUI txt = tmp.GetComponent<TextMeshProUGUI>();
        if(tutorial.activeSelf) {
            tutorial.SetActive(false);
            return;
        }

        if(gameObject.activeSelf) {
            OnHitButton.Invoke("SETTINGS");
            gameObject.SetActive(false);
            if(clearProgress.activeSelf) {
                clearProgress.SetActive(false);
            }
        } else {
            OnHitButton.Invoke("BACK");
            gameObject.SetActive(true);
        }
    }

    public void ShowHideClearProgress() {
        SoundsAction.ButtonClick();
        if(clearProgress.activeSelf) {
            clearProgress.SetActive(false);
        } else {
            clearProgress.SetActive(true);
        }
    }

    public void ClearData() {
        SoundsAction.ButtonClick();
        PlayerProfile.ClearPrefs();
        Application.Quit();
    }
    public void LoadSpec() {
        loadCtl++;
        StartCoroutine(loadSpecWait());
    }

    public IEnumerator loadSpecWait() {
        if(loadCtl > 8) {
            SceneManager.LoadScene(2);
        }
        yield return new WaitForSeconds(5f);
        loadCtl = 0;
    }
}
