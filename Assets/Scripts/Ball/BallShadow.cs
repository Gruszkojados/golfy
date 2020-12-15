using UnityEngine;

public class BallShadow : MonoBehaviour // Rotation for ball shadow.
{
    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
            transform.rotation = rotation;
    }
}
