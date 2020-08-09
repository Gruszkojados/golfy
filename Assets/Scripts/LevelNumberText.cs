using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumberText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();  // pobieranie komponentu po jego typie
        LvlController.OnLvlLoaded += SetLevelNumber;
    }
    void OnDestroy() {
        LvlController.OnLvlLoaded -= SetLevelNumber;
    }

    public void SetLevelNumber(Lvl _, int level) {
        textMeshProUGUI.text = (level+1).ToString();
    }
}
