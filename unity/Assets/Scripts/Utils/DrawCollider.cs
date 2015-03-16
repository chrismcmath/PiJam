using UnityEngine;
using System.Collections;

public class DrawCollider : MonoBehaviour {
    public Color ColliderColor = Color.yellow;

    private PolygonCollider2D _Collider;

    public void GetCollider() {
        _Collider = GetComponent<PolygonCollider2D>();
    }

    public void OnDrawGizmos() {
        if (_Collider == null) {
            GetCollider();
        }

        Gizmos.color = ColliderColor;

        Vector2[] points = _Collider.points;
        for (int i = 0; i < points.Length; i++) {
            Vector2 point = points[i];

            Vector2 nextPoint;
            if (i < points.Length - 1) {
                nextPoint = points[i+1];
            } else {
                nextPoint = points[0];
            }
            Gizmos.DrawLine(GetPoint(point), GetPoint(nextPoint));
        }
    }

    private Vector2 GetPoint(Vector2 p) {
        return p + (Vector2) transform.position;
    }
}
