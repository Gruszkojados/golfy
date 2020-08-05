using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlScore : MonoBehaviour
{   
    public LevelData allScores;
    public static event Action<int> OnScoreChange = (number) => {};
    public static event Action<LevelData> OnLoadScore = (allScores) => {};
    int numberOfLvlScore = 0;

    private void Awake() {
        Debug.Log("LvlScore Awake()");
        LoadScore();
        foreach (var item in allScores.scoreList)
        {
            Debug.Log("Wczytany score: " + item);
        }
        allScores = new LevelData();
        ShootButton.OnShoot += IncrementLvlScore;
        LvlController.OnLvlComplited += addIndexAndScoreList;
        LvlController.OnLvlLoaded += ClearScore;
        LvlController.OnGoToHome += addIndexAndScoreList;
    }

    private void OnDestroy() {
        SaveScore();
        ShootButton.OnShoot -= IncrementLvlScore;
        LvlController.OnLvlComplited -= addIndexAndScoreList;
        LvlController.OnLvlLoaded -= ClearScore;
    }

    void addIndexAndScoreList(int lvlIndex) {
        if(allScores.scoreList.Count<=lvlIndex) {
            allScores.scoreList.Add(numberOfLvlScore);
        } else if (allScores.scoreList.Count>=lvlIndex) {
            if(allScores.scoreList[lvlIndex]<numberOfLvlScore) {
                allScores.scoreList[lvlIndex] = numberOfLvlScore;
            }
        }
    }

    void LoadScore() {
        allScores = LoadData();
        LogReadWrite(false);
        OnLoadScore.Invoke(allScores);
    }

    

    public LevelData LoadData() {
        string json = PlayerPrefs.GetString("allScores");
        LevelData allScoresTemp = JsonUtility.FromJson<LevelData>(json);
        return allScoresTemp;
    } 

    void SaveScore() {
        LogReadWrite(true);
        PlayerPrefs.SetString("allScores", JsonUtility.ToJson(allScores));
    }

    void IncrementLvlScore(float nothing) {
        numberOfLvlScore++;
        OnScoreChange.Invoke(numberOfLvlScore);
    }

    void ClearScore(Lvl levelUnused) {
        numberOfLvlScore = 0;
    }

    void LogReadWrite(bool isSaveing) {
        string highScores = "";
        foreach (var item in allScores.scoreList){
            highScores += (" " + item.ToString());
        }
        if(isSaveing) {
            Debug.Log("Zapisano postęp: " + highScores);
        } else {
            Debug.Log("Wczytano postęp: " + highScores);
        }
    }
}

public class LevelData
{
    public LevelData() {
        scoreList = new List<int>();
    }
    public List<int> scoreList;
}