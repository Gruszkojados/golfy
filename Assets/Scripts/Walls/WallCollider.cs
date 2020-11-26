using System;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{   
    public static event Action<Vector3[]> OnLineRender = (vectorTable) => {};
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    List<Vector2> points = new List<Vector2>();
    void Start() // function to load points for main wall collider
    {   
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        foreach(var point in positions) {
            Vector2 pointTmp = new Vector2(point.x, point.y);
            points.Add(pointTmp);
        }
        edgeCollider2D.points = points.ToArray();
        OnLineRender.Invoke(positions);
        AstarPath.active.Scan();
    }
}
