using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{   
    public static event Action onBallInHole = () => {};
    public GameObject ball;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Ball"){
            ball.SetActive(false);
            onBallInHole.Invoke();
        }
    }
}