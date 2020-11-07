using UnityEngine;

public class LvlButtonList : MonoBehaviour
{   
    public LvlButton lvlButtonPrefab;
    void Start()
    {   
        LevelData levelData = LevelData.LoadData();
        for(int i=0; i<=levelData.scoreList.Count; i++) {
            Instantiate(lvlButtonPrefab, transform).Setup("Lvl " + (i+1).ToString(), i);
        }
    }
}
