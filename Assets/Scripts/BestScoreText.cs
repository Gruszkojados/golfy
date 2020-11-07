using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    void Awake() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetBestScoreText(int bestScore) {
        textMeshProUGUI.text = bestScore.ToString();
    }
}
