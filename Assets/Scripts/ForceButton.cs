using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceButton : MonoBehaviour
{
    public static event Action OnChangeForce = () => { };

    public GameObject[] objectsToEnable;

    public void Setfoce() {
        foreach (var item in objectsToEnable)
        {
            item.SetActive(true);
        }
        OnChangeForce.Invoke();
        gameObject.SetActive(false);
    }

    private void Awake() {
        ShootButton.OnShoot += HideButton;
        Ball.OnBallStop += ShowForceButton;
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= HideButton;
        Ball.OnBallStop -= ShowForceButton;
    }

    void HideButton(float power) {
        gameObject.SetActive(false);
    }

    void ShowForceButton() {
        gameObject.SetActive(true);
    }
}
