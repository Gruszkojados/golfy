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
        //Debug.Log("Awake dla pilki");
        rigid = GetComponent<Rigidbody2D>();
        ForceButton.OnChangeForce += StopBallRotate;
        seeker = GetComponent<Seeker>();
        OnBallChangePosition.Invoke(transform.position);
    }

    private void OnDestroy() {
        if(isSupscribed) {
            //Debug.Log("Players human DESTROY");
            ShootButton.OnShoot -= Shoot;
            TouchInput.OnDrag -= Rotate;
        }
        ForceButton.OnChangeForce -= StopBallRotate;
    }

    public void SupscribeTouchCtl() {
        //Debug.Log("Player subskrybuje shoot i rotation");
        ShootButton.OnShoot += Shoot;
        TouchInput.OnDrag += Rotate;
        isSupscribed = true;
    }

    private void Update() {
        if(!isMoving) {
            animator.SetBool("isMove", false);
            return;
        }
        OnBallChangePosition.Invoke(transform.position);

        if(rigid.velocity.sqrMagnitude < 0.2f) {
            isMoving = false;
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
        //Debug.Log("dlaczego ten shoot dziala Shoot ?!");
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
