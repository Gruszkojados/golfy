using UnityEngine;

public class Ramp : MonoBehaviour
{   
    public string rampDirection = "down";
    PolygonCollider2D colider;
    private void Awake() {
        colider = gameObject.GetComponent<PolygonCollider2D>();
        AiPlayer.SwitchColider += ColiderSwitch;
    }
    private void OnDestroy() {
        AiPlayer.SwitchColider -= ColiderSwitch;
    } 
    void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Ball") {
            Ball ball = other.GetComponent<Ball>();
            ball.RollBack(rampDirection);
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
