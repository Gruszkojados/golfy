using UnityEngine;

public class SpinBar : MonoBehaviour
{
    public Vector3 vector = new Vector3(0f,0f,100f);
    void Update() { // spin bar rotate
        transform.Rotate(vector * Time.deltaTime);
    }
    
}
