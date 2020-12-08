using UnityEngine;

public class Dancer : MonoBehaviour
{
    public float maxDistance = 10f;
    new Rigidbody2D rigidbody;
    int sign = 1;
    private void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start() { // Dancer start move
        rigidbody.AddForce(new Vector2(600,0));
    }
    private void OnCollisionEnter2D(Collision2D other) { // Dancer change move direction
        if(other.collider.tag!="Ball") {
            sign *= -1;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(new Vector2(600 * sign,0));
        }
    }
}
