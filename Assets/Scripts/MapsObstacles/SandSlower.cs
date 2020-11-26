using System;
using UnityEngine;

public class SandSlower : MonoBehaviour
{   
    PolygonCollider2D colider;
    private void Awake() {
        colider = gameObject.GetComponent<PolygonCollider2D>();
        AiPlayer.SwitchColider += ColiderSwitch;
    }
    private void OnDestroy() {
        AiPlayer.SwitchColider -= ColiderSwitch;
    }
    private void OnTriggerStay2D(Collider2D other) { // trigger on ball stary on slower
        if(other.tag=="Ball") {
            Ball ball = other.gameObject.GetComponent<Ball>();
            ball.SlowVelocity();
        }
    }
    void ColiderSwitch() {
        if(colider.enabled) {
            colider.enabled = false;
        } else {
            colider.enabled = true;
        }
    }
}
