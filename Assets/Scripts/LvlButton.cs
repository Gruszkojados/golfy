using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlButton : MonoBehaviour
{   
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
        PlayerProfile.levelIndex = levelIndex;
        SceneManager.LoadScene(1);
    }
}