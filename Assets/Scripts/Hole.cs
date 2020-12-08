using System;
using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{   
    public GameObject slowDown;
    Animator animSlowDown;
    public static event Action<bool, bool> onBallInHole = (isBot, scoreTargetLimit) => {};
    public static event Action<Vector2> HoleInitioalPosition  = (vector) => {};
    Vector2 holePosition;
    public GameObject fullHole;
    public GameObject emptyHoll;
    int currentScore = 0;
    int currentTargetScore = 0;
    private void Awake() {
        Ball.OnAnyBallStop += WhereIsHole;
        LvlScore.OnScoreChange += UpdateCurrentScore;
        LvlController.OnLvlLoaded += UpdateCurrentTargetScore;
    }
    private void Start() {
        animSlowDown = slowDown.GetComponent<Animator>();
        ChageHollImage();
        holePosition = new Vector2(transform.position.x, transform.position.y);
        WhereIsHole();
    }

    private void OnDestroy() {
        Ball.OnAnyBallStop -= WhereIsHole;
        LvlScore.OnScoreChange -= UpdateCurrentScore;
        LvlController.OnLvlLoaded -= UpdateCurrentTargetScore;
    }

    public void WhereIsHole() { 
        HoleInitioalPosition.Invoke(holePosition);
    }
    private void OnTriggerEnter2D(Collider2D other) { // Ball-in-hole - function.
        if(other.tag=="Ball") {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if(ball==null) {
                return;
            }
            if(ball.velocity > 1500) { // Checking ball speed for swing.
                SoundsAction.BallSwing();
                animSlowDown.SetTrigger("Slow");
                ball.SmallChangeDirection();
                return;
            }
            ball.gameObject.SetActive(false);
            ChageHollImage();
            SoundsAction.BallInHole();
            StartCoroutine(Wait(ball));
        }
    }
    void ChageHollImage() { // Change hole image.
        if(fullHole.activeSelf) {
            fullHole.SetActive(false);
            emptyHoll.SetActive(true);
        } else {
            fullHole.SetActive(true);
            emptyHoll.SetActive(false);
        }
        
    }

    void UpdateCurrentScore(int currentScore) { // Updating current score for "single" mode.
        this.currentScore = currentScore;
    }
    void UpdateCurrentTargetScore(Lvl currentLvl, int _) { // Updating target score for "single" mode.
        this.currentTargetScore = currentLvl.targetOfShoots;
    }

    public IEnumerator Wait(Ball ball) {
        yield return new WaitForSeconds(0.5f);
        ChageHollImage();
        onBallInHole.Invoke(ball.getOwner(), ScoreDifference());
    }

    bool ScoreDifference() { // Checking is game complete, only "single" mode.
        return currentScore > currentTargetScore ? true : false;
    }
}