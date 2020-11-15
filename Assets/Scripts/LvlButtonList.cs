using UnityEngine;

public class LvlButtonList : MonoBehaviour
{   
    public LvlButton lvlButtonPrefab;
    RectTransform rectTransform;
    
    void Start()
    {   
        LevelData levelData = LevelData.LoadData();
        int loadedLvls = levelData.scoreList.Count;
        for(int i=0; i<=loadedLvls; i++) {
            Instantiate(lvlButtonPrefab, transform).Setup("Lvl " + (i+1).ToString(), i);
        }
        rectTransform = gameObject.GetComponent<RectTransform>();
        if(loadedLvls > 7) {
            int addHight = loadedLvls - 7; 
            rectTransform.sizeDelta = new Vector2(0, 500f + (addHight*80));
        }
    }
}
