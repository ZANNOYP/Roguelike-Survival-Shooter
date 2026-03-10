using UnityEngine;

[ExecuteAlways]
public class PlayerBoundaryGizmo : MonoBehaviour
{
    public Vector2 minBound = new Vector2(-20, -20);
    public Vector2 maxBound = new Vector2(20, 20);
    public Color gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 bottomLeft = new Vector3(minBound.x, minBound.y, 0f);
        Vector3 topLeft = new Vector3(minBound.x, maxBound.y, 0f);
        Vector3 topRight = new Vector3(maxBound.x, maxBound.y, 0f);
        Vector3 bottomRight = new Vector3(maxBound.x, minBound.y, 0f);

        // »­±ß½ç¿ò
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}
