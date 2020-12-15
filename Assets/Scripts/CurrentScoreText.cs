using TMPro;
using UnityEngine;

public class CurrentScoreText : MonoBehaviour // Class for display current score.
{   
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
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

    void ClearScore(Lvl _, int __) {
        textMeshProUGUI.text = "0";
    }


}
