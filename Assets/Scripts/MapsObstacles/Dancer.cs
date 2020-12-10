using UnityEngine;

public class Dancer : MonoBehaviour
{
    Rigidbody2D dancerRigidbody;
    int sign = 1;
    private void Awake() {
        dancerRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start() { // Dancer start move
        dancerRigidbody.AddForce(new Vector2(600,0));
    }
    private void OnCollisionEnter2D(Collision2D other) { // Dancer change move direction
        if(other.collider.tag!="Ball") {
            sign *= -1;
            dancerRigidbody.velocity = Vector2.zero;
            dancerRigidbody.AddForce(new Vector2(600 * sign,0));
        }
    }
}
