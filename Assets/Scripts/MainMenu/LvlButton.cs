using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlButton : MonoBehaviour
{   
    public static event Action OnLoadLvlButton = () => {};
    public TextMeshProUGUI textMeshProUGUI_Lvl;
    public TextMeshProUGUI textMeshProUGUI_BestScore;

    int levelIndex;

    public void Setup(string name, int index, int bestScore) {
        textMeshProUGUI_Lvl.text = name;
        string bestScoreText = bestScore == 1000 ? "" : "best score: " + bestScore.ToString();
        textMeshProUGUI_BestScore.text = bestScoreText;
        levelIndex = index;
    }

    public void LoadLevel() {
        SoundsAction.ButtonClick();
        PlayerProfile.levelIndex = levelIndex;
        StartCoroutine(LoadLvlWait());
    }

    IEnumerator LoadLvlWait() {
        OnLoadLvlButton.Invoke();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}