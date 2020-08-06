﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlScore : MonoBehaviour
{   
    public static event Action<int> OnScoreChange = (number) => {};
    public static event Action<LevelData> OnLoadScore = (allScores) => {};

    int numberOfLvlScore = 0;
    public LevelData allScores;
    public BestScoreText bestScoreText;
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

    void UpdateHighScore() {
        if(allScores.scoreList.Count > currentLvlIndex) {
            if(allScores.scoreList[currentLvlIndex] > numberOfLvlScore) {
                allScores.scoreList[currentLvlIndex] = numberOfLvlScore;
            }
        } else {
            allScores.scoreList.Add(numberOfLvlScore);
        }
        bestScoreText.SetBestScoreText(allScores.scoreList[currentLvlIndex]);
        allScores.SaveScore();
    }

    void LoadScore() {
        allScores = LevelData.LoadData();
        OnLoadScore.Invoke(allScores);
    }

    void IncrementLvlScore(float nothing) {
        numberOfLvlScore++;
        OnScoreChange.Invoke(numberOfLvlScore);
    }

    void OnLvlLoaded(Lvl _, int index) {
        numberOfLvlScore = 0;
        currentLvlIndex = index;
    }

    
}

[Serializable]
public class LevelData
{
    public LevelData() {
        scoreList = new List<int>();
    }
    public List<int> scoreList;
    const string classKey = "allScores";

    public static LevelData LoadData() {
        if(!PlayerPrefs.HasKey(classKey)) {
            new LevelData().SaveScore();
        };
        string json = PlayerPrefs.GetString(classKey);
        LevelData allScoresTemp = JsonUtility.FromJson<LevelData>(json);
        
        return allScoresTemp;
    } 

    public void SaveScore() {
        PlayerPrefs.SetString(classKey, JsonUtility.ToJson(this));
    }

    // dokoncz potem sobie jedrek xd
    void LogReadWrite(bool isSaveing) {
        string highScores = "";
        foreach (var item in scoreList){
            highScores += (" " + item.ToString());
        }
        if(isSaveing) {
            Debug.Log("Zapisano postęp: " + highScores);
        } else {
            Debug.Log("Wczytano postęp: " + highScores);
        }
    }

}