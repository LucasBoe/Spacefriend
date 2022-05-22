using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationLink : MonoBehaviour
{
    [SerializeField] NavigationPoint[] points = new NavigationPoint[2];
    public NavigationPoint[] Points => points;

    private void OnDrawGizmos()
    {
        if (points[0] != null && points[1] != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(points[0].transform.position, points[1].transform.position);
        }
    }

    internal Vector3 ToLine(Vector3 target)
    {
        Vector2 vector2 = Vector2Util.GetClosestPointOnLineSegment(points[0].transform.position, points[1].transform.position, target);
        return new Vector3(vector2.x, vector2.y, 0);
    }
}
