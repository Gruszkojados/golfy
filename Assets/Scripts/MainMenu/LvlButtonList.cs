﻿using UnityEngine;

public class LvlButtonList : MonoBehaviour
{   
    public LvlButton lvlButtonPrefab;
    RectTransform rectTransform;
    
    void Start() // function init all available levels buttons and format level list in GUI
    {   
        LevelData levelData = LevelData.LoadData();
        int loadedLvls = levelData.scoreList.Count;
        Debug.Log("Tryb: " + PlayerProfile.gameMode);
        for(int i=0; i<=loadedLvls; i++) {
            if(loadedLvls>i){
                Instantiate(lvlButtonPrefab, transform).Setup("Lvl " + (i+1).ToString(), i, levelData.scoreList[i]);
            } else {
                Instantiate(lvlButtonPrefab, transform).Setup("Lvl " + (i+1).ToString(), i, 1000);
            }
        }
        rectTransform = gameObject.GetComponent<RectTransform>();
        if(loadedLvls > 7) {
            int addHight = loadedLvls - 7; 
            rectTransform.sizeDelta = new Vector2(0, 500f + (addHight*100));
        }
    }
}
