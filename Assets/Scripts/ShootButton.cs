using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButton : MonoBehaviour
{
    public static event Action<float> OnShoot = (power) => { };

    public ChargeBar chargeBar;
    public void Shoot(){
        OnShoot(chargeBar.chargePower);
        gameObject.SetActive(false);
    }

    private void Awake() {
        gameObject.SetActive(false);
    }

    void HideButton() {
        gameObject.SetActive(false);
    }

}
