using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using golf;
using System;

public class Ball : MonoBehaviour
{   

    public static event Action OnBallStop = () => { };
    Rigidbody2D rigid;
    bool isMoving;
    public GameObject rotationBar;

    bool canRotate = true;
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        ShootButton.OnShoot += Shoot;
        TouchInput.OnDrag += Rotate;
        ForceButton.OnChangeForce += StopBallRotate;
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= Shoot;
        TouchInput.OnDrag -= Rotate;
        ForceButton.OnChangeForce -= StopBallRotate;
    }

    private void Update() {
        if(!isMoving) {
            return;
        }
        if(rigid.velocity.sqrMagnitude < 0.4f) {
            isMoving = false;
            ShowRotationBar();
            LetBallRotate();
            OnBallStop.Invoke();
        }
    }

    void Rotate(float rotate) {
        if(!canRotate) {
            return;
        }
        if(rotate>0) {
            RotateRight(rotate);
        } else {
            RotateLeft(rotate);
        }
    }
    public void Shoot(float power) {
        isMoving = true;
        HideRotationBar();
        rigid.AddForce(transform.up  * power, ForceMode2D.Impulse);
    }

    public void RotateLeft(float rotaterForce) {
        transform.Rotate(new Vector3(0,0,1 * rotaterForce));
    }
    public void RotateRight(float rotaterForce) {
        transform.Rotate(new Vector3(0,0,-1 * -rotaterForce));
    }
    void HideRotationBar() {
        rotationBar.SetActive(false);
    }
    void ShowRotationBar() {
        rotationBar.SetActive(true);
    }

    void LetBallRotate() {
        Debug.Log("start rotate");
        canRotate = true;
    }

    void StopBallRotate() {
        Debug.Log("stop rotate");
        canRotate = false;
    }
}
