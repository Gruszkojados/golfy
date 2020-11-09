using UnityEngine;
using golf;
using System;
using Pathfinding;

public class Ball : MonoBehaviour
{   
    public float velocity;
    public static event Action OnAnyBallStop = () => {};
    public event Action<Ball> OnBallStoped = (_) => {};
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
    bool isBotBall = false;
    bool canRotate = true;
    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        ForceButton.OnChangeForce += StopBallRotate;
        seeker = GetComponent<Seeker>();
        OnBallChangePosition.Invoke(transform.position);
    }

    void OnDestroy() {
        if(isSupscribed) {
            ShootButton.OnShoot -= Shoot;
            TouchInput.OnDrag -= Rotate;
        }
        ForceButton.OnChangeForce -= StopBallRotate;
    }

    public void SupscribeTouchCtl() {
        ShootButton.OnShoot += Shoot;
        TouchInput.OnDrag += Rotate;
        isSupscribed = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.GetType()!=typeof(Hole) && other.GetType()!=typeof(Ball)) {
            SoundsAction.Bounce();
        }
    }
    void Update() {
        if(!isMoving) {
            // animator.SetBool("isMove", false);
            return;
        }        
        OnBallChangePosition.Invoke(transform.position);
        velocity = rigid.velocity.sqrMagnitude;
        if(velocity < 0.2f) {
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
        Shoot(power, transform.up);
    }

    public void Shoot(float power, Vector2 direction) {
        SoundsAction.Shoot();
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

    public void setOwner(bool isBot) {
        isBotBall = isBot;
    }
    public bool getOwner() {    
        return isBotBall;
    }

    public void SmallChangeDirection() {
        // TODO
        // tutaj jakos ogarnąć jakies losowe odchylenie pilki jesli nadleci ze zbyt duza sila w dolek ...
    }

    public void SlowVelocity() {
        rigid.velocity = rigid.velocity * 0.9f;
    }
}
