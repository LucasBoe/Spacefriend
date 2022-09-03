using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sprouts.Physics
{
    public class RoomPhysics : MonoBehaviour
    {
        [ReadOnly] public List<Vector2> RoomBoundaries = new List<Vector2>();
        [ReadOnly] private Vector2 RoomCenter = Vector2.zero;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField, Range(-3, 3)] float gravity;
        public void UpdateGravity()
        {
            PhysicsBehaviour[] physicsBehavioursInRoom = GetComponentsInChildren<PhysicsBehaviour>();
            physicsBehavioursInRoom.Each<PhysicsBehaviour>(p => p.UpdateGravity(gravity, RoomCenter));
        }

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            RoomCenter = CalculateRoomCenter(RoomBoundaries);
            AddEdgeCollider(RoomBoundaries);
            lineRenderer.UpdatePoints(RoomBoundaries);
        }

        private Vector2 CalculateRoomCenter(List<Vector2> roomBoundaries)
        {
            Vector2 center = Vector2.zero;
            foreach (Vector2 v2 in roomBoundaries)
            {
                center += v2;
            }
            return center /= roomBoundaries.Count;
        }

        private void AddEdgeCollider(List<Vector2> roomBoundaries)
        {
            roomBoundaries.Add(roomBoundaries[0]);
            gameObject.AddChildWithComponent<EdgeCollider2D>("Boundaries").SetPoints(roomBoundaries);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(RoomCenter, 0.5f);
        }
    }
}
