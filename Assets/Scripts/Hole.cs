using System;
using UnityEngine;

public class Hole : MonoBehaviour
{   
    public static event Action onBallInHole = () => {};
    Vector2 holePosition;
    public static event Action<Vector2> HoleInitioalPosition  = (vector) => {};

    private void Start() {
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
        if(other.tag=="Ball"){
            Ball ball = other.gameObject.GetComponent<Ball>();
            if(ball==null) {
                return;
            }
            ball.gameObject.SetActive(false);
            onBallInHole.Invoke();
        }
    }
}