using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{
    NavigationLink[] links;
    Vector3[] debugPoints;

    private void Awake()
    {
        links = GetComponentsInChildren<NavigationLink>();
    }

    internal List<Vector3> GetPath(Vector3 start, Vector3 target)
    {

        NavigationLink startLink = GetClosestLinkTo(start);
        NavigationLink targetLink = GetClosestLinkTo(target);

        List<Vector3> path = new List<Vector3>();

        List<NavigationNode> openSet = new List<NavigationNode>();
        HashSet<NavigationNode> closeSet = new HashSet<NavigationNode>();

        openSet.Add(new NavigationNode() { Link = startLink });

        while (openSet.Count > 0)
        {
            NavigationNode current = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < current.FCost || openSet[i].FCost == current.FCost && openSet[i].HCost < current.HCost)
                    current = openSet[i];
            }

            openSet.Remove(current);
            closeSet.Add(current);

            if (current.Link == targetLink)
            {
                NavigationPoint previous = current.GetSharedPoint(current.Parent);
                path.Add(previous.transform.position);

                current = current.Parent;
                
                while (current.Link != startLink)
                {
                    NavigationPoint next = current.Link.GetOtherPoint(previous);

                    Debug.Log(next.name);

                    path.Add(next.transform.position);
                    previous = next;
                    current = current.Parent;
                }

                path.Reverse();

                path.Add(targetLink.ToLine(target));

                debugPoints = path.ToArray();
                return path;
            }

            foreach (NavigationNode neightbour in GetNeightbours(current, links))
            {
                if (closeSet.Contains(neightbour)) continue;

                float newCost = current.GCost + neightbour.GetDistance(current.Link);
                if (newCost < neightbour.GCost || !openSet.Contains(neightbour))
                {
                    neightbour.GCost = newCost;
                    neightbour.HCost = neightbour.GetDistance(targetLink);
                    neightbour.Parent = current;

                    if (!openSet.Contains(neightbour))
                        openSet.Add(neightbour);
                }
            }
        }

        return path;
    }
    private List<NavigationNode> GetNeightbours(NavigationNode node, NavigationLink[] links)
    {
        List<NavigationNode> list = new List<NavigationNode>();

        HashSet<NavigationPoint> points = new HashSet<NavigationPoint>(node.Link.Points);

        foreach (NavigationLink link in links)
        {
            if (link != node.Link)
            {
                foreach (NavigationPoint point in link.Points)
                {
                    if (points.Contains(point))
                        list.Add(new NavigationNode() { Link = link });
                }
            }
        }

        return list;
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

    private class NavigationNode
    {
        public NavigationLink Link;
        public float GCost, HCost;
        public float FCost => GCost + HCost;
        public NavigationNode Parent;

        internal float GetDistance(NavigationLink neightbour)
        {
            return Vector2.Distance(Vector2.Lerp(Link.Points[0].transform.position, Link.Points[1].transform.position, 0.5f), Vector2.Lerp(neightbour.Points[0].transform.position, neightbour.Points[1].transform.position, 0.5f));
        }

        internal NavigationPoint GetSharedPoint(NavigationNode parent)
        {
            foreach (NavigationPoint point in Link.Points)
            {
                foreach (NavigationPoint p in parent.Link.Points)
                {
                    if (p == point) return point;
                }
            }

            return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (debugPoints != null && debugPoints.Length != 0)
        {
            for (int i = 1; i < debugPoints.Length - 1; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(debugPoints[i- 1], debugPoints[i]);
            }
        }
    }
}
