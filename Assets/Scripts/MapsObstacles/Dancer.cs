using UnityEngine;

public class Dancer : MonoBehaviour
{
    public float maxDistance = 10f;
    new Rigidbody2D rigidbody;
    private void Awake() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start() {
        rigidbody.AddForce(new Vector2(-500,0));
    }
    void FixedUpdate()
    {   
        if(gameObject.transform.position.x > maxDistance) {
            rigidbody.AddForce(new Vector2(-500,0));
        }
        if(gameObject.transform.position.x < -maxDistance) {
            rigidbody.AddForce(new Vector2(500,0));
        }
    }
}
