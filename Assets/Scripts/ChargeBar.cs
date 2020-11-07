using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    bool up = true;
    float chargeValue = 1f;
    public float chargePower { 
        get { 
            return chargeValue/powerDivider; 
        } 
    }
    public float powerDivider = 4f;
    public float iterMax = 50f;
    public float iterMin = 10f;
    public float chargeValueChange = 20f;

    private void Awake() {
        gameObject.SetActive(false);
        ShootButton.OnShoot += HideChargeBar;
    }
    void Update()
    {
        if(up && chargeValue<=iterMax) {
            chargeValue += chargeValueChange * Time.deltaTime;
            if(chargeValue>iterMax) {
                up = false;
            }
        } else if(!up && chargeValue>=iterMin) {
            chargeValue -= chargeValueChange * Time.deltaTime;
            if(chargeValue<iterMin) {
                up = true;
            }
        }
        transform.localScale = new Vector3(20, chargeValue, 0);
    }

    void HideChargeBar(float _) {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= HideChargeBar;
    }
}
