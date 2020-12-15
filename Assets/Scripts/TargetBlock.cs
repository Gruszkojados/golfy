using UnityEngine;

public class TargetBlock : MonoBehaviour
{
    public void ChangeStatus() {
        if(gameObject.activeSelf) {
            gameObject.SetActive(false);
        } else {
             gameObject.SetActive(true);
        }
    }

    public void SetPosition(Vector2 vec) { // Change position of block. AiAplyer is aim on it when he want to bounce.
        gameObject.transform.SetPositionAndRotation(new Vector3(vec.x, vec.y, 0), new Quaternion());
    }
}
