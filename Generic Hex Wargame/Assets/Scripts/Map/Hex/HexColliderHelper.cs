using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexColliderHelper : MonoBehaviour
{
    public PolygonCollider2D polygonCollider2D;

    public float GetColliderHeight()
    {
        Bounds bounds = GetPolygonColliderBounds(polygonCollider2D);
        float height = bounds.size.y;
        return height;
    }

    public float GetColliderWidth()
    {
        Bounds bounds = GetPolygonColliderBounds(polygonCollider2D);
        float width = bounds.size.x;
        return width;
    }

    private Bounds GetPolygonColliderBounds(PolygonCollider2D polygonCollider)
    {
        Vector2[] points = polygonCollider.points;

        if(points.Length == 0)
        {
            return new Bounds();
        }

        Vector2 min = points[0];
        Vector2 max = points[0];

        foreach(Vector2 point in points)
        {
            min = Vector2.Min(min, point);
            max = Vector2.Max(max, point);
        }

        // Transform the local bounds to world space
        min = polygonCollider.transform.TransformPoint(min);
        max = polygonCollider.transform.TransformPoint(max);

        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);

        return bounds;
    }
}
