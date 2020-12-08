using UnityEngine;
using golf;
using Pathfinding;

public class Ball : MonoBehaviour
{   
    public float velocity;
    public static event System.Action OnAnyBallStop = () => {};
    public event System.Action<Ball> OnBallStoped = (_) => {};
    public static event System.Action<Vector2> OnBallChangePosition = (vec) => {};
    Rigidbody2D rigid;
    bool isMoving;
    public GameObject rotationBar;
    public Collider2D ballColider;
    [HideInInspector]
    public Seeker seeker;
    public TargetBlock targetBlock;
    bool isSupscribed = false;
    [HideInInspector]
    bool isBotBall = false;
    bool canRotate = true;
    bool isOnRamp = false;
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
    void OnCollisionEnter2D(Collision2D other) {
        if(other.GetType()!=typeof(Hole) && other.GetType()!=typeof(Ball)) {
            SoundsAction.Bounce();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Ramp") {
            isOnRamp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Ramp") {
            isOnRamp = false;
        }
    }
    void Update() {
        
        if(!isMoving) {
            return;
        }        
        OnBallChangePosition.Invoke(transform.position);
        velocity = rigid.velocity.sqrMagnitude;
        if(isOnRamp) {
            return;
        }
        if(velocity < 0.4f) {
            rigid.velocity = new Vector2(0,0);
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

    void RotateLeft(float rotaterForce) {
        transform.Rotate(new Vector3(0,0,1 * rotaterForce));
    }
    void RotateRight(float rotaterForce) {
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

    /*### functions for obstacles ###*/

    // function for holes
    public void SmallChangeDirection() { 
        if(Random.Range(0f, 2f) > 1) {
            rigid.AddForce(new Vector2(-1000f, 500f));
        } else {
            rigid.AddForce(new Vector2(1000f, 500f));
        }
    }

    // function for ball slowers
    public void SlowVelocity() {
        rigid.velocity = rigid.velocity * 0.9f;
    }

    // function for ramps
    public void RollBack(string rampDirection) {
        if(rampDirection=="down") {
            rigid.AddForce(new Vector2(20f, -60f));
        } else if (rampDirection=="up") {
            rigid.AddForce(new Vector2(20f, 60f));
        } else if (rampDirection=="left") {
            rigid.AddForce(new Vector2(-60f, -20f));
        } else if (rampDirection=="right") {
            rigid.AddForce(new Vector2(60f, -20f));
        }
        
    }
}
