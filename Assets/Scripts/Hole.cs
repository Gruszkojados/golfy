using System;
using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{   
    public static event Action<bool> onBallInHole = (isBot) => {};
    Vector2 holePosition;
    public static event Action<Vector2> HoleInitioalPosition  = (vector) => {};
    public GameObject fullHole;
    public GameObject emptyHoll;
    private void Start() {
        ChageHollImage();
        Ball.OnAnyBallStop += WhereIsHole;
        holePosition = new Vector2(transform.position.x, transform.position.y);
        WhereIsHole();
    }

    private void OnDestroy() {
        Ball.OnAnyBallStop -= WhereIsHole;
    }

    public void WhereIsHole() { 
        HoleInitioalPosition.Invoke(holePosition);
    }
    private void OnTriggerStay2D(Collider2D other) {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Ball") {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if(ball==null) {
                return;
            }
            if(ball.velocity > 1500) {
                return;
            }
            ball.gameObject.SetActive(false);
            ChageHollImage();
            SoundsAction.BallInHole();
            StartCoroutine(Wait(ball));
        }
    }
    void ChageHollImage() {
        if(fullHole.activeSelf) {
            fullHole.SetActive(false);
            emptyHoll.SetActive(true);
        } else {
            fullHole.SetActive(true);
            emptyHoll.SetActive(false);
        }
    }
        public IEnumerator Wait(Ball ball) {
        yield return new WaitForSeconds(0.5f);
        ChageHollImage();
        onBallInHole.Invoke(ball.getOwner());
    }
}