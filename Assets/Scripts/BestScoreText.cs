using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    LevelData allScores;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();  // pobieranie komponentu po jego typie
        LvlController.OnBestScore += OnLvlLoaded;
        LvlScore.OnLoadScore += LoadAllScores;
    }

    private void OnDestroy() {
        LvlController.OnBestScore -= OnLvlLoaded;
        LvlScore.OnLoadScore -= LoadAllScores;
    }

    void OnLvlLoaded(int index) {
        Debug.Log("przypisuje wartosc najlepszego uderzenia");
        if(allScores.scoreList.Count>index) {

            textMeshProUGUI.text = allScores.scoreList[index].ToString();
        } else {
            textMeshProUGUI.text = "0";
        }
        
    }

    void LoadAllScores(LevelData allScores) {
        this.allScores = allScores;
    }
}
