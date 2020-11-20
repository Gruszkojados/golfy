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

    public void SetBestScoreText(int bestScore) {
        if(bestScore==1000) {
            textMeshProUGUI.text = "...";
        } else {
            textMeshProUGUI.text = bestScore.ToString();
        }
    }
}
