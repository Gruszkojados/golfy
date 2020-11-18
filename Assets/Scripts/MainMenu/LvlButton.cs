using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlButton : MonoBehaviour
{   
    public static event Action OnLoadLvlButton = () => {};
    TextMeshProUGUI textMeshProUGUI;
    int levelIndex;
    private void Awake() {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(string name, int index) {
        textMeshProUGUI.text = name;
        levelIndex = index;
    }

    public void LoadLevel() {
        SoundsAction.ButtonClick();
        PlayerProfile.levelIndex = levelIndex;
        StartCoroutine(LoadLvlWait());
    }

    public IEnumerator LoadLvlWait() {
        OnLoadLvlButton.Invoke();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}