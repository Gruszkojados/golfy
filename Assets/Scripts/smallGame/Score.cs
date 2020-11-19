using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    int score = 0;
    [SerializeField] int bestScore;
    private void Awake() {
        LoadScore();
        Enemy.OnDie += UpdateScore;
        tmp.text = score.ToString() + " / " + bestScore.ToString();
    }
    private void OnDestroy() {
        if(score>bestScore) {
            SaveScore();
        }
        Enemy.OnDie -= UpdateScore;
    }
    void UpdateScore() {
        score++;
        tmp.text = score.ToString() + " / " + bestScore.ToString();
    }

    void SaveScore() {
        PlayerPrefs.SetInt("miniGameScore", score);
    }

    void LoadScore() {
        if(!PlayerPrefs.HasKey("miniGameScore")) {
            Debug.Log("No score before");
            SaveScore();
        };
        bestScore = PlayerPrefs.GetInt("miniGameScore");
    }
}
