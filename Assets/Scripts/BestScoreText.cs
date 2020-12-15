using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        LvlScore.OnBestScoreLoaded += SetBestScoreText;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    void OnDestroy() {
        LvlScore.OnBestScoreLoaded -= SetBestScoreText;
    }

    public void SetBestScoreText(int bestScore) { // Seting best score on panel
        if(bestScore==1000) {
            textMeshProUGUI.text = "...";
        } else {
            textMeshProUGUI.text = bestScore.ToString();
        }
    }
}
