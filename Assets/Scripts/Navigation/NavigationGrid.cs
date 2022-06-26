using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationGrid : MonoBehaviour
{
    protected INavigationLink[] links;
    Vector3[] debugPoints;

    protected virtual void Awake()
    {
        links = GetComponentsInChildren<INavigationLink>();
    }

    internal List<Vector3> GetPath(Vector3 start, Vector3 target)
    {

        INavigationLink startLink = GetClosestLinkTo(start);
        INavigationLink targetLink = GetClosestLinkTo(target);

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
                if (current.Parent != null)
                {
                    INavigationPoint previous = current.GetSharedPoint(current.Parent);
                    path.Add(previous.Position);

                    current = current.Parent;

                    while (current.Link != startLink)
                    {
                        INavigationPoint next = current.Link.GetOtherPoint(previous);

                        path.Add(next.Position);
                        previous = next;
                        current = current.Parent;
                    }

                    path.Reverse();
                }

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
    private List<NavigationNode> GetNeightbours(NavigationNode node, INavigationLink[] links)
    {
        List<NavigationNode> list = new List<NavigationNode>();

        HashSet<INavigationPoint> points = new HashSet<INavigationPoint>(node.Link.Points);

        foreach (INavigationLink link in links)
        {
            if (link != node.Link)
            {
                foreach (INavigationPoint point in link.Points)
                {
                    if (points.Contains(point))
                        list.Add(new NavigationNode() { Link = link });
                }
            }
        }

        return list;
    }

    private INavigationLink GetClosestLinkTo(Vector3 point)
    {
        float dist = float.MaxValue;
        INavigationLink link = null;

        foreach (INavigationLink l in links)
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
        public INavigationLink Link;
        public float GCost, HCost;
        public float FCost => GCost + HCost;
        public NavigationNode Parent;

        internal float GetDistance(INavigationLink neightbour)
        {
            return Vector2.Distance(Vector2.Lerp(Link.Points[0].Position, Link.Points[1].Position, 0.5f), Vector2.Lerp(neightbour.Points[0].Position, neightbour.Points[1].Position, 0.5f));
        }

        internal INavigationPoint GetSharedPoint(NavigationNode parent)
        {
            foreach (INavigationPoint point in Link.Points)
            {
                foreach (INavigationPoint p in parent.Link.Points)
                {
                    if (p == point) return point;
                }
            }

            return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        foreach (INavigationLink links in GetComponentsInChildren<INavigationLink>())
            Gizmos.DrawLine(links.Points[0].Position, links.Points[1].Position);
    }
}

public interface INavigationLink
{
    INavigationPoint[] Points { get; }

    INavigationPoint GetOtherPoint(INavigationPoint previous);
    Vector3 ToLine(Vector3 target);
}

public interface INavigationPoint
{
    Vector3 Position { get; }
}
