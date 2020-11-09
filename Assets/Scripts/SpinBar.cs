using UnityEngine;

public class SpinBar : MonoBehaviour
{
    public Vector3 vector = new Vector3(0f,0f,100f);
    void Update() {
        transform.Rotate(vector * Time.deltaTime);
    }
    
}
