using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{   
    public static event Action OnHomeBack = () => {}; 
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
    public GameObject animator;
    Animator transition;
    void Start()
    {
        transition = animator.GetComponent<Animator>();
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
    void LoadLvl(LoadLevelType loadLvlType) { // function loading correct level. Continue game, next level or restart level
        HideQuickGameMenu(0);
        
        if(currentLvl!=null) {      
            Destroy(currentLvl.gameObject);
        }
        if(loadLvlType==LoadLevelType.nextLvl) {
            lvlIndex++;
        } else if (loadLvlType==LoadLevelType.fromPlayerProfile){
            lvlIndex = PlayerProfile.levelIndex;
            
        }

        if(lvlList.levels.Length < lvlIndex+1) { // if complete whole game, return to main scene
                SceneManager.LoadScene(0);
        }

        currentLvl = Instantiate(lvlList.levels[lvlIndex]);
        lvlComplitedObject.SetActive(false);
        lvlLostObject.SetActive(false);
        forceButton.SetActive(true);
        OnLvlLoaded.Invoke(currentLvl, lvlIndex);
    }

    void lvlComplited(bool isBot, bool scoreLimit) { // correct messages in panel after game 
        if(isBot) {
            TextMeshProUGUI tmp = lvlLostObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = "Bot won !";
            SoundsAction.Lost();
            lvlLostObject.SetActive(true);
        } else if(scoreLimit && PlayerProfile.gameMode == Gamemode.singlePlayer) {
            TextMeshProUGUI tmp = lvlLostObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = "You have reached shoots limit !";
            SoundsAction.Lost();
            lvlLostObject.SetActive(true);
        } else {
            SoundsAction.Win();
            lvlComplitedObject.SetActive(true);
        }
    }

    public void GoToHome() {
        SoundsAction.ButtonClick();
        StartCoroutine(LoadLvlWait());
    }

    public IEnumerator LoadLvlWait() { // loading screen delay and change scene 
        OnHomeBack.Invoke();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    public void ShowQuickGameMenu() { // show/hide quick menu in game
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

enum LoadLevelType // type of loaded level
{
    currentLvl = 0,          // restart level
    nextLvl = 1,             // next level
    fromPlayerProfile = 2    // continue game
}
