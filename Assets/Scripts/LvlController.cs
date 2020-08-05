using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{   
    public static event Action<Lvl> OnLvlLoaded = (level) => {};
    public static event Action<int> OnLvlComplited = (index) => {};
    public static event Action<int> OnGoToHome = (index) => {};
    public static event Action<int> OnBestScore = (bestScore) => {};
    public List<Lvl> lvlList;
    Lvl currentLvl;
    int lvlIndex;
    public GameObject lvlComplitedObject;
    public GameObject forceButton;
    void Start()
    {
        Reloadlvl();
    }

    void Awake() {
        Hole.onBallInHole += lvlComplited;
    }

    private void OnDestroy() {
        Hole.onBallInHole -= lvlComplited;
    }
    public void ChangeLvl() {
        OnLvlComplited.Invoke(lvlIndex);
        LoadLvl(true);
    }

    public void Reloadlvl() {
        LoadLvl(false);
    }

    void LoadLvl(bool incrementLvlIndex) {
        lvlComplited();
        if(currentLvl!=null) {
            Destroy(currentLvl.gameObject);
        }
        if(incrementLvlIndex) {
            lvlIndex++;
        }
        currentLvl = Instantiate(lvlList[lvlIndex]);
        lvlComplitedObject.SetActive(false);
        forceButton.SetActive(true);
        OnLvlLoaded.Invoke(currentLvl);
    }

    void lvlComplited() {
        lvlComplitedObject.SetActive(true);
    }

    public void GoToHome() {
        OnGoToHome.Invoke(lvlIndex);
        SceneManager.LoadScene(0);
    }
}
