using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();  // pobieranie komponentu po jego typie
    }

    public void SetBestScoreText(int bestScore) {
        textMeshProUGUI.text = bestScore.ToString();
    }
}
