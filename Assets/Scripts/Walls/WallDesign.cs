using UnityEngine;

public class WallDesign : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private void Awake() {
        WallCollider.OnLineRender += SetLines;
    }
    private void OnDestroy() {
        WallCollider.OnLineRender -= SetLines;
    }
    void SetLines(Vector3[] vecTable)
    {   
        lineRenderer.positionCount = vecTable.Length;
        lineRenderer.SetPositions(vecTable);
    }
}

