using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationLinkBehaviour : MonoBehaviour, INavigationLink
{
    [SerializeField] NavigationPointBehaviour[] points = new NavigationPointBehaviour[2];

    INavigationPoint[] INavigationLink.Points => points;

    //private void OnDrawGizmos()
    //{
    //    if (points[0] != null && points[1] != null)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(points[0].transform.position, points[1].transform.position);
    //    }
    //}

    public Vector3 ToLine(Vector3 target)
    {
        Vector2 vector2 = Vector2Util.GetClosestPointOnLineSegment(points[0].transform.position, points[1].transform.position, target);
        return new Vector3(vector2.x, vector2.y, 0);
    }

    internal Vector3 GetNextPoint(Vector3 current, Vector3 target)
    {
        float d1 = Vector2.Distance(current, points[0].transform.position);
        float d2 = Vector2.Distance(current, points[1].transform.position);

        if (d1 < 0.01f)
            return points[1].transform.position;
        else if (d2 < 0.01f)
            return points[0].transform.position;
        else
            return ToLine(target);


    }

    internal NavigationPointBehaviour GetFurthestPoint(Vector3 target)
    {
        if (Vector2.Distance(target, points[0].transform.position) < Vector2.Distance(target, points[1].transform.position))
            return points[1];
        else
            return points[0];
    }

    public INavigationPoint GetOtherPoint(INavigationPoint previous)
    {
        if (points[0].Position == previous.Position)
            return points[1];
        else
            return points[0];
    }
}
