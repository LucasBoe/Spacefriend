using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{
    NavigationLink[] links;

    private void Awake()
    {
        links = GetComponentsInChildren<NavigationLink>();
    }

    internal List<Vector3>GetPath(Vector3 start, Vector3 target)
    {
        List<Vector3> path = new List<Vector3>();
        path.Add(start);

        NavigationLink startLink = GetClosestLinkTo(start);
        NavigationLink endLink = GetClosestLinkTo(target);

        path.Add(endLink.ToLine(target));

        return path;
    }

    private NavigationLink GetClosestLinkTo(Vector3 point)
    {
        float dist = float.MaxValue;
        NavigationLink link = null;

        foreach (NavigationLink l in links)
        {
            float distance = Vector2.Distance(point, l.ToLine(point));


            if (distance < dist)
            {
                dist = distance;
                link = l;
            }
        }

        Debug.DrawLine(point, link.ToLine(point));

        return link;
    }
}
