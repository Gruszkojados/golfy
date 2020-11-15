using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{   
    public static event Action<Lvl, int> OnLvlLoaded = (level, index) => {};
    public static event Action<int> OnLvlComplited = (index) => {};
    public static event Action<float> OnShowQuickMenu = (_) => {};
    public static event Action OnHideQuickMenu = () => {};
    public static event Action OnResetLvl = () => {};
    public LvlList lvlList;
    Lvl currentLvl;
    int lvlIndex;
    public GameObject lvlComplitedObject;
    public GameObject lvlLostObject;
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
        SoundsAction.ButtonClick();
        OnLvlComplited.Invoke(lvlIndex);
        LoadLvl(LoadLevelType.nextLvl);
    }
    public void Reloadlvl() {
        SoundsAction.ButtonClick();
        OnResetLvl.Invoke();
        LoadLvl(LoadLevelType.currentLvl);
    }
    void LoadLvl(LoadLevelType loadLvlType) {
        HideQuickGameMenu(0);
        
        if(currentLvl!=null) {      
            Destroy(currentLvl.gameObject);
        }
        if(loadLvlType==LoadLevelType.nextLvl) {
            lvlIndex++;
        } else if (loadLvlType==LoadLevelType.fromPlayerProfile){
            lvlIndex = PlayerProfile.levelIndex;
            
        }

        if(lvlList.levels.Length < lvlIndex+1) {
                SceneManager.LoadScene(0);
        }

        currentLvl = Instantiate(lvlList.levels[lvlIndex]);
        lvlComplitedObject.SetActive(false);
        lvlLostObject.SetActive(false);
        forceButton.SetActive(true);
        OnLvlLoaded.Invoke(currentLvl, lvlIndex);
    }

    void lvlComplited(bool isBot) {
        if(isBot) {
            SoundsAction.Lost();
            lvlLostObject.SetActive(true);
        } else {
            SoundsAction.Win();
            lvlComplitedObject.SetActive(true);
        }
    }

    public void GoToHome() {
        SoundsAction.ButtonClick();
        SceneManager.LoadScene(0);  
    }

    public void ShowQuickGameMenu() {
        SoundsAction.ButtonClick();
        if(quickMenuObject.activeSelf) {
            quickMenuObject.SetActive(false);
        } else {
            quickMenuObject.SetActive(true);
        }
        
        OnShowQuickMenu.Invoke(0);
    }

    public void HideQuickGameMenu(float _) {
        SoundsAction.ButtonClick();
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
