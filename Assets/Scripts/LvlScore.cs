using System;
using System.Collections.Generic;
using UnityEngine;

public class LvlScore : MonoBehaviour
{   
    public static event Action<int> OnScoreChange = (number) => {};
    public static event Action<int, int> OnLoadScore = (currentLvl, actualScore) => {};
    public static event Action<int> OnBestScoreLoaded = (bestScore) => {};

    int numberOfLvlScore = 0;
    public LevelData allScores;
    int currentLvlIndex;

    private void Awake() {
        ShootButton.OnShoot += IncrementLvlScore;
        LvlController.OnLvlLoaded += OnLvlLoaded;
        Hole.onBallInHole += UpdateHighScore;
    }

    private void Start() {
        LoadScore();
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= IncrementLvlScore;
        LvlController.OnLvlLoaded -= OnLvlLoaded;
        Hole.onBallInHole -= UpdateHighScore;
    }

    void UpdateHighScore(bool isBot, bool scoreLimit) { // Updating level score if is new best score
        try
        {
            if(!isBot && !scoreLimit) {
                if(allScores.scoreList.Count > currentLvlIndex) {
                    if(allScores.scoreList[currentLvlIndex] > numberOfLvlScore) {
                        allScores.scoreList[currentLvlIndex] = numberOfLvlScore;
                    }
                } else {
                    allScores.scoreList.Add(numberOfLvlScore);
                }
                OnBestScoreLoaded.Invoke(allScores.scoreList[currentLvlIndex]);
                allScores.SaveScore();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Index: " + allScores.scoreList[currentLvlIndex] + "LvlScore error: " + e.Message);
            throw;
        }
    }

    void LoadScore() {
        allScores = LevelData.LoadData();
    }

    void IncrementLvlScore(float nothing) {
        numberOfLvlScore++;
        OnScoreChange.Invoke(numberOfLvlScore);
    }

    void OnLvlLoaded(Lvl _, int index) { // Event for sending info about current level best score
        numberOfLvlScore = 0;
        currentLvlIndex = index;    
        var highScore = currentLvlIndex >= allScores.scoreList.Count ? 10 : allScores.scoreList[currentLvlIndex];
        OnLoadScore.Invoke(currentLvlIndex, highScore);
        if(allScores.scoreList.Count>=currentLvlIndex+1) {
            OnBestScoreLoaded.Invoke(allScores.scoreList[currentLvlIndex]);
        } else {
            OnBestScoreLoaded.Invoke(1000);
        }
    }
}

[Serializable]
public class LevelData // Class for read and update scores. Using PlayerPrefs.
{
    public LevelData() {
        scoreList = new List<int>();
    }
    public List<int> scoreList;
    const string classKey = "allScores";

    public static LevelData LoadData() {
        if(!PlayerPrefs.HasKey(classKey)) { 
            var ld = new LevelData();
            ld.scoreList.Add(1000);
            ld.SaveScore();
        };
        string json = PlayerPrefs.GetString(classKey);
        LevelData allScoresTemp = JsonUtility.FromJson<LevelData>(json);
        return allScoresTemp;
    } 

    public void SaveScore() {
        PlayerPrefs.SetString(classKey, JsonUtility.ToJson(this));
    }
}