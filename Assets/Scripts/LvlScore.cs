using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlScore : MonoBehaviour
{   
    public LevelData allScores;
    public static event Action<int> OnScoreChange = (number) => {};
    int numberOfLvlScore = 0;

    private void Awake() {
        LoadScore();
        ShootButton.OnShoot += IncrementLvlScore;
        LvlController.OnLvlComplited += addIndexAndScoreList;
        LvlController.OnLvlLoaded += ClearScore;
        LvlController.OnGoToHome += addIndexAndScoreList;
        allScores = new LevelData();
    }

    private void OnDestroy() {
        SaveScore(allScores);
        ShootButton.OnShoot -= IncrementLvlScore;
        LvlController.OnLvlComplited -= addIndexAndScoreList;
        LvlController.OnLvlLoaded -= ClearScore;
    }

    void addIndexAndScoreList(int lvlIndex) {
        if(allScores.scoreList.Count<=lvlIndex) {
            allScores.scoreList.Add(numberOfLvlScore);
        } else if (allScores.scoreList.Count>=lvlIndex) {
            allScores.scoreList[lvlIndex] = numberOfLvlScore;
        }
    }

    void LoadScore() {
        string json = PlayerPrefs.GetString("allScores");
        allScores = JsonUtility.FromJson<LevelData>(json);
    }

    void SaveScore(LevelData allScores) {
        Debug.Log("zapisano postęp");
        PlayerPrefs.SetString("allScores", JsonUtility.ToJson(allScores));
    }

    void IncrementLvlScore(float nothing) {
        numberOfLvlScore++;
        OnScoreChange.Invoke(numberOfLvlScore);
    }

    void ClearScore(Lvl levelUnused) {
        numberOfLvlScore = 0;
    }
}

public class LevelData
{
    public LevelData() {
        scoreList = new List<int>();
    }
    public List<int> scoreList;
}