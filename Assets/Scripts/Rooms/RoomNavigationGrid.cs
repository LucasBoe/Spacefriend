using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigationGrid : NavigationGrid
{
    [SerializeField] Room room;
    [SerializeField] bool useEdgeColliderOverride = false;
    [ShowIf("useEdgeColliderOverride"), SerializeField] EdgeCollider2D edgeColliderOverride;

    protected override void Awake()
    {
        if (useEdgeColliderOverride)
        {
            Vector2[] points = edgeColliderOverride.points;
            List<INavigationLink> newLinks = new List<INavigationLink>();

            NavigationPointInfo previous = new NavigationPointInfo(points[0]);

            for (int i = 1; i < points.Length; i++)
            {
                NavigationPointInfo point = new NavigationPointInfo(points[i]);
                newLinks.Add(new NavigationLinkInfo(previous, point));
            }

            links = newLinks.ToArray();
            Destroy(edgeColliderOverride);
        }
        else
        {
            base.Awake();
        }
    }

    private void OnValidate()
    {
        room = GetComponentInParent<Room>();
    }

    private void OnEnable()
    {
        room.SetRoomStateEvent += OnSetRoomStateEvent;
    }

    private void OnDisable()
    {
        room.SetRoomStateEvent -= OnSetRoomStateEvent;
    }

    private void OnSetRoomStateEvent(bool roomActive)
    {
        if (roomActive) NavigationAgent.TriggerSwitchEvent(this);
    }

    private class NavigationPointInfo : INavigationPoint
    {
        private Vector3 position;
        public Vector3 Position => position;
        public NavigationPointInfo(Vector3 pos)
        {
            position = pos;
        }
    }

    private class NavigationLinkInfo : INavigationLink
    {
        public NavigationPointInfo[] points;
        public INavigationPoint[] Points => points;

        public NavigationLinkInfo(NavigationPointInfo point1, NavigationPointInfo point2)
        {
            points = new NavigationPointInfo[] { point1, point2 };
        }

        public INavigationPoint GetOtherPoint(INavigationPoint previous)
        {
            if (points[0].Position == previous.Position)
                return points[1];
            else
                return points[0];
        }

        public Vector3 ToLine(Vector3 target)
        {
            Vector2 vector2 = Vector2Util.GetClosestPointOnLineSegment(points[0].Position, points[1].Position, target);
            return new Vector3(vector2.x, vector2.y, 0);
        }
    }
}
