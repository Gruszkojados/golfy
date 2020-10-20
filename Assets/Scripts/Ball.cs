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
    public static event Action<Vector2> OnBallChangePosition = (vec) => {};
    Rigidbody2D rigid;
    bool isMoving;
    public GameObject rotationBar;
    public Collider2D ballColider;
    [HideInInspector]
    public Seeker seeker;
    public Animator animator;
    bool isSupscribed = false;
    [HideInInspector]
    bool canRotate = true;
    void Awake() {
        Debug.Log("Awake dla pilki");
        rigid = GetComponent<Rigidbody2D>();
        ForceButton.OnChangeForce += StopBallRotate;
        seeker = GetComponent<Seeker>();
    }

    private void OnDestroy() {
        if(isSupscribed) {
            Debug.Log("pilka została odsubskrybowana");
            ShootButton.OnShoot -= Shoot;
            TouchInput.OnDrag -= Rotate;
        }
        ForceButton.OnChangeForce -= StopBallRotate;
    }

    public void SupscribeTouchCtl() {
        Debug.Log("Zaczyna HumanPlayer, pilka zostala zasubskrybowana");
        isSupscribed = true;
        ShootButton.OnShoot += Shoot;
        TouchInput.OnDrag += Rotate;
    }

    private void Update() {
        
        if(!isMoving) {
            animator.SetBool("isMove", false);
            return;
        }
        OnBallChangePosition.Invoke(transform.position);

        if(rigid.velocity.sqrMagnitude < 0.4f) {
            isMoving = false;
            LetBallRotate();
            OnAnyBallStop.Invoke();
            OnBallStoped.Invoke(this);
        }
    }

    void Rotate(float rotate) {
        Debug.Log("Rotate dziala");
        if(!canRotate) {
            //Debug.Log("Can't rotate");
            return;
        }
        //Debug.Log("Rotate");
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
        Debug.Log("dlaczego ten shoot dziala Shoot");
        Shoot(power, transform.up);
    }

    public void Shoot(float power, Vector2 direction) {
        isMoving = true;
        RotationBarDisplay(false);
        rigid.AddForce(direction * power, ForceMode2D.Impulse);
    }

    public void RotateLeft(float rotaterForce) {
        transform.Rotate(new Vector3(0,0,1 * rotaterForce));
    }
    public void RotateRight(float rotaterForce) {
        transform.Rotate(new Vector3(0,0,-1 * -rotaterForce));
    }
    public void RotationBarDisplay(bool isVisible) {
        rotationBar.SetActive(isVisible);
    }

    void LetBallRotate() {
        canRotate = true;
    }

    void StopBallRotate() {
        canRotate = false;
    }
}
