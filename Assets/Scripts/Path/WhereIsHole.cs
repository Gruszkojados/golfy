using UnityEngine;

public class WhereIsHole : MonoBehaviour
{
    private void Awake() {
        Hole.HoleInitioalPosition += SetHolePosiotion;
    }
    private void OnDestroy() {
        Hole.HoleInitioalPosition -= SetHolePosiotion;
    }
    void SetHolePosiotion(Vector2 vec) {
        transform.SetPositionAndRotation(new Vector3(vec.x, vec.y, 0), new Quaternion());
    }
}
