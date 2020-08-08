using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSlower : MonoBehaviour
{   
    public static event Action OnBallEnter = () => {};
    public static event Action OnBallExit = () => {};
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Ball"){
            OnBallEnter.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Ball"){
            OnBallExit.Invoke();
        }
    }

}
