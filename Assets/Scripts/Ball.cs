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
    public float rotationSpeed = 40f;
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        ShootButton.OnShoot += Shoot;
        TouchInput.OnDrag += Rotate;
    }

    private void OnDestroy() {
        ShootButton.OnShoot -= Shoot;
        TouchInput.OnDrag -= Rotate;
    }

    private void Update() {
        if(!isMoving) {
            return;
        }
        if(rigid.velocity.sqrMagnitude < 0.4f) {
            isMoving = false;
            ShowRotationBar();
            OnBallStop.Invoke();
        }
    }

    void Rotate(float rotate) {
        if(rotate>0) {
            RotateRight();
        } else {
            RotateLeft();
        }
    }
    public void Shoot(float power) {
        isMoving = true;
        HideRotationBar();
        rigid.AddForce(transform.up  * power, ForceMode2D.Impulse);
    }

    public void RotateLeft() {
        transform.Rotate(new Vector3(0,0,1 * rotationSpeed));
    }
    public void RotateRight() {
        transform.Rotate(new Vector3(0,0,-1 * rotationSpeed));
    }
    void HideRotationBar() {
        rotationBar.SetActive(false);
    }
    void ShowRotationBar() {
        rotationBar.SetActive(true);
    }
}
