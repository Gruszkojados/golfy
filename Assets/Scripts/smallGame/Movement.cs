using UnityEngine;

public class Movement : MonoBehaviour
{    
    public float distance = 0.05f;
    void FixedUpdate()
    {   
        if(Input.GetAxis("Horizontal")<0) {
            transform.position = new Vector2(gameObject.transform.position.x - distance, 0);
        }
        if(Input.GetAxis("Horizontal")>0) {
            transform.position = new Vector2(gameObject.transform.position.x + distance, 0);
        }
    }
}
