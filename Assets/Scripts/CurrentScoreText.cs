using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentScoreText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();  // pobieranie komponentu po jego typie
        LvlScore.OnScoreChange += OnLvlLoaded;
        LvlController.OnLvlLoaded += ClearScore;
    }

    private void OnDestroy() {
        LvlScore.OnScoreChange -= OnLvlLoaded;
        LvlController.OnLvlLoaded -= ClearScore;
    }

    void OnLvlLoaded(int number) {
        textMeshProUGUI.text = number.ToString();
    }

    void ClearScore(Lvl number) {
        textMeshProUGUI.text = "0";
    }


}
