using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuickHomeButton : MonoBehaviour
{   
    private void Awake() {
        Ball.OnAnyBallStop += HideButton;
        HumanPlayer.OnHumanStartTurn += ShowButton;
    }

    private void OnDestroy() {
        HumanPlayer.OnHumanStartTurn -= ShowButton;
        Ball.OnAnyBallStop -= HideButton;
    }

    void ShowButton() {
        //gameObject.SetActive(true);
    }

    void HideButton() {
        //gameObject.SetActive(false);
    }
}
