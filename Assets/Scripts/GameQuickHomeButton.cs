using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuickHomeButton : MonoBehaviour
{   
    private void Awake() {
        ShootButton.OnShoot += HideButton;
        Ball.OnAnyBallStop += Show_1;
        LvlController.OnLvlLoaded += Show_2;
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= HideButton;
        Ball.OnAnyBallStop -= Show_1;
        LvlController.OnLvlLoaded -= Show_2;
    }

    void ShowButton() {
        gameObject.SetActive(true);
    }

    void Show_1() {
        ShowButton();
    }
    void Show_2(Lvl _, int __) {
        ShowButton();
    }

    void HideButton(float _) {
        gameObject.SetActive(false);
    }
}
