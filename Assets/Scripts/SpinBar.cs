using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBar : MonoBehaviour
{
    Vector3 vector;
    void Start() {
        vector = new Vector3(0f,0f,6f);    
    }
    void FixedUpdate() {
        transform.Rotate(vector);
    }
    
}
