using UnityEngine;

public class BallFollower : MonoBehaviour // calls for update ball position. Useing by camerFollower
{   
    public GameObject follower;
    private void Awake() {
        Ball.OnBallChangePosition += ChangePosition;
        PlayerController.OnChangePlayer += ChangePosition;
    }
    private void OnDestroy() {
        Ball.OnBallChangePosition -= ChangePosition;
        PlayerController.OnChangePlayer -= ChangePosition;
    }

    void ChangePosition(Vector2 vec) {
        follower.transform.position = vec;
    }

    void ChangePosition(Player player) {
        ChangePosition(player.ball.transform.position);
    }
}
