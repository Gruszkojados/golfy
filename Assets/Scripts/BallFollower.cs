using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollower : MonoBehaviour
{   
    public GameObject follower;
    private void Awake() {
        Ball.OnBallChangePosition += ChangePosition;
    }
    private void OnDestroy() {
        Ball.OnBallChangePosition -= ChangePosition;
    }

    void ChangePosition(float x, float y) {
        follower.transform.position = new Vector3(x,y,0);
    }
}
