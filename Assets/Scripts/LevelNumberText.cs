using TMPro;
using UnityEngine;

public class LevelNumberText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        LvlController.OnLvlLoaded += SetLevelNumber;
    }
    void OnDestroy() {
        LvlController.OnLvlLoaded -= SetLevelNumber;
    }

    public void SetLevelNumber(Lvl _, int level) { // Seting current level number.
        textMeshProUGUI.text = (level+1).ToString();
    }
}
