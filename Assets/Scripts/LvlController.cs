using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{   
    public static event Action<Lvl, int> OnLvlLoaded = (level, index) => {};
    public static event Action<int> OnLvlComplited = (index) => {};
    public static event Action<float> OnShowQuickMenu = (_) => {};
    public static event Action OnHideQuickMenu = () => {};
    public LvlList lvlList;
    Lvl currentLvl;
    int lvlIndex;
    public GameObject lvlComplitedObject;
    public GameObject quickMenuObject;
    public GameObject forceButton;
    void Start()
    {
        LoadLvl(LoadLevelType.fromPlayerProfile);
    }

    void Awake() {
        Hole.onBallInHole += lvlComplited;
    }

    private void OnDestroy() {
        Hole.onBallInHole -= lvlComplited;
    }
    public void ChangeLvl() {
        OnLvlComplited.Invoke(lvlIndex);
        LoadLvl(LoadLevelType.nextLvl);
    }

    

    public void Reloadlvl() {
        LoadLvl(LoadLevelType.currentLvl);
    }

    void LoadLvl(LoadLevelType loadLvlType) {
        HideQuickGameMenu(0);
        lvlComplited();
        if(currentLvl!=null) {      
            Destroy(currentLvl.gameObject);
        }
        if(loadLvlType==LoadLevelType.nextLvl) {
            lvlIndex++;
        } else if (loadLvlType==LoadLevelType.fromPlayerProfile){
            lvlIndex = PlayerProfile.levelIndex;
        }
        currentLvl = Instantiate(lvlList.levels[lvlIndex]);
        lvlComplitedObject.SetActive(false);
        forceButton.SetActive(true);
        Debug.Log("Załadowano poziom !");
        OnLvlLoaded.Invoke(currentLvl, lvlIndex);
    }

    void lvlComplited() {
        lvlComplitedObject.SetActive(true);
    }

    public void GoToHome() {
        SceneManager.LoadScene(0);
    }

    public void ShowQuickGameMenu() {
        quickMenuObject.SetActive(true);
        OnShowQuickMenu.Invoke(0);
    }

    public void HideQuickGameMenu(float _) {
        quickMenuObject.SetActive(false);
        OnHideQuickMenu.Invoke();
    }
}

enum LoadLevelType
{
    currentLvl = 0,
    nextLvl = 1,
    fromPlayerProfile = 2
}
