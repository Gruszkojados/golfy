using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();  // pobieranie komponentu po jego typie
        LvlController.OnLvlLoaded += OnLvlLoaded;
    }

    private void OnDestroy() {
        LvlController.OnLvlLoaded -= OnLvlLoaded;
    }

    void OnLvlLoaded(Lvl level) {
        textMeshProUGUI.text = level.targetOfShoots.ToString();
    }
}
