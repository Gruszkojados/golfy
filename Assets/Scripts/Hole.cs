using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{   
    public static event Action onBallInHole = () => {};
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