using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{   
    public static event Action OnColiderDrowed = () => {};
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    List<Vector2> points = new List<Vector2>();
    void Start()
    {   
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        
        foreach(var point in positions) {
            Vector2 pointTmp = new Vector2(point.x, point.y);
            points.Add(pointTmp);
        }
        edgeCollider2D.points = points.ToArray();
        AstarPath.active.Scan();
    }
}
