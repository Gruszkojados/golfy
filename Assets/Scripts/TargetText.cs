using UnityEngine;
using TMPro;

public class TargetText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        LvlController.OnLvlLoaded += OnLvlLoaded;
    }

    private void OnDestroy() {
        LvlController.OnLvlLoaded -= OnLvlLoaded;
    }

    void OnLvlLoaded(Lvl level, int _) {
        textMeshProUGUI.text = level.targetOfShoots.ToString();
    }
}
