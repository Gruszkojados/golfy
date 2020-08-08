using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickHomeButton : MonoBehaviour
{
    private void Awake() {
        Hole.onBallInHole += HideButton;
        LvlController.OnLvlLoaded += ShowButton;
    }
    private void OnDestroy() {
        Hole.onBallInHole -= HideButton;
        LvlController.OnLvlLoaded -= ShowButton;
    }
    void HideButton() {
        gameObject.SetActive(false);
    }
    void ShowButton(Lvl _, int __) {
        gameObject.SetActive(true);
    }

    
}
