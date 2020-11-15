using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapch : MonoBehaviour
{
    public void Draw() {
        Debug.DrawLine(new Vector2(10, 10),  new Vector2(110, 110), Color.red, 10);
        Debug.DrawLine(new Vector2(110, 110),  new Vector2(90, 10), Color.red, 10);
        Debug.DrawLine(new Vector2(90, -210),  new Vector2(90, 10), Color.red, 10);
    }
}
