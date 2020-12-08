using UnityEngine;

public class LvlButtonList : MonoBehaviour
{   
    public LvlButton lvlButtonPrefab;
    RectTransform rectTransform;
    LevelData levelData;
    void Start() // function init all available levels buttons and format level list in GUI
    {   
        levelData = LevelData.LoadData();
        int loadedLvls = levelData.scoreList.Count;
        for(int i=0; i<=loadedLvls; i++) {
            if(loadedLvls>i){
                Instantiate(lvlButtonPrefab, transform).Setup("Lvl " + (i+1).ToString(), i, levelData.scoreList[i]);
            } else {
                Instantiate(lvlButtonPrefab, transform).Setup("Lvl " + (i+1).ToString(), i, 1000);
            }
        }


        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(0, (loadedLvls+1) * 70f);
        rectTransform.anchoredPosition = new Vector2(0,(loadedLvls+1) * 70f);
    }
}