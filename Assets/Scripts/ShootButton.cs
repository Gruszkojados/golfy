using System;
using UnityEngine;

public class ShootButton : MonoBehaviour
{
    public static event Action<float> OnShoot = (power) => { };

    public ChargeBar chargeBar;
    public void Shoot() { // shoot button behavior
        OnShoot.Invoke(chargeBar.chargePower);
        gameObject.SetActive(false);
    }

    private void Awake() {
        gameObject.SetActive(false);
        LvlController.OnResetLvl += HideButton;
    }
    private void OnDestroy() {
        LvlController.OnResetLvl -= HideButton;
    }

    void HideButton() {
        gameObject.SetActive(false);
    }
}
