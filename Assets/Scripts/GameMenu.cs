using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{   
    public static event Action OnPlayButtonClick = () => {};
    private void Awake() {
        MenuLvlList.OnReturnToMenuButton += ShowButtons;
    }
    private void OnDestroy() {
        MenuLvlList.OnReturnToMenuButton -= ShowButtons;
    }
    public void HideButtons() {
        OnPlayButtonClick.Invoke();
        gameObject.SetActive(false);
    }

    void ShowButtons() {
        gameObject.SetActive(true);
    }
}
