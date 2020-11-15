using UnityEngine;

public class Dancer : MonoBehaviour
{
    public float move = 1f;
    public float maxDistance = 8f;
    bool directionCtrl = true;
    void Update()
    {   
        if(directionCtrl) {
            Move(true);
            if(gameObject.transform.position.x > maxDistance) {
                directionCtrl = false;
            }
        } else {
            Move(false);
            if(gameObject.transform.position.x < maxDistance) {
                directionCtrl = true;
            }
        }
    }

    void Move(bool direction) {
        if(direction) {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + move, 0, 0);
        } else {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - move, 0, 0);
        }
    }
}
