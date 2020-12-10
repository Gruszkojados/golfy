using UnityEngine;

public class Shoot : MonoBehaviour
{
    Rigidbody2D buletRigidbody;
    Vector2 forceVec = new Vector2(0,300f);
    void Start()
    {
        SoundsAction.BallSwing();
        buletRigidbody = gameObject.GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, 0);
        buletRigidbody.AddForce(forceVec);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Ball") {
            Destroy(gameObject);
        }
    }
}
