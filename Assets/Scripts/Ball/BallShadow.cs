using UnityEngine;

public class BallShadow : MonoBehaviour
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
