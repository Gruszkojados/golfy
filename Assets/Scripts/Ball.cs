using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using golf;
using System;
using Pathfinding;

public class Ball : MonoBehaviour
{   
    public static event Action OnAnyBallStop = () => { };
    public event Action<Ball> OnBallStoped = (_) => { }; 
    public static event Action<float, float> OnBallChangePosition = (x, y) => {};
    Rigidbody2D rigid;
    bool isMoving;
    public GameObject rotationBar;
    public Collider2D ballColider;
    [HideInInspector]
    public Seeker seeker;
    public Animator animator;
    bool isSupscribed = false;

    bool canRotate = true;
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        ForceButton.OnChangeForce += StopBallRotate;
        seeker = GetComponent<Seeker>();
    }

    private void Start() {
        OnBallChangePosition.Invoke(transform.position.x, transform.position.y);
    }

    private void OnDestroy() {
        if(isSupscribed) {
            ShootButton.OnShoot -= Shoot;
            TouchInput.OnDrag -= Rotate;
        }
        ForceButton.OnChangeForce -= StopBallRotate;
    }

    public void SupscribeTouchCtl() {
        isSupscribed = true;
        ShootButton.OnShoot += Shoot;
        TouchInput.OnDrag += Rotate;
    }

    private void Update() {
        
        if(!isMoving) {
            animator.SetBool("isMove", false);
            return;
        }
        OnBallChangePosition.Invoke(transform.position.x, transform.position.y);

        if(rigid.velocity.sqrMagnitude < 0.4f) {
            isMoving = false;
            ShowRotationBar();
            LetBallRotate();
            OnAnyBallStop.Invoke();
            OnBallStoped.Invoke(this);
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

    public void ActivateBall(bool isactive) {
        ballColider.enabled = isactive;
    }

    public void Shoot(float power) {
        Shoot(power, transform.up);
    }

    public void Shoot(float power, Vector2 direction) {
        isMoving = true;
        HideRotationBar();
        rigid.AddForce(direction * power, ForceMode2D.Impulse);
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
        canRotate = true;
    }

    void StopBallRotate() {
        canRotate = false;
    }
}
