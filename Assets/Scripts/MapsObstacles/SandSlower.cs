using System;
using UnityEngine;

public class SandSlower : MonoBehaviour
{   
    public static event Action OnBallEnter = () => {};
    public static event Action OnBallExit = () => {};
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Ball") {
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.SlowVelocity();
        }
    }
}
