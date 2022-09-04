using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sprouts.Physics
{
    [RequireComponent(typeof(RoomBehaviour))]
    public class RoomPhysics : MonoBehaviour
    {
        [ReadOnly] public List<Vector2> RoomBoundaries = new List<Vector2>();
        [SerializeField, ReadOnly] private Vector2 RoomCenter = Vector2.zero;
        [SerializeField, ReadOnly] private EdgeCollider2D edgeCollider;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField, Range(-3, 3)] float gravity;
        [SerializeField, ReadOnly] RoomBehaviour roomBehaviour;
        private void Awake()
        {
            roomBehaviour = GetComponent<RoomBehaviour>();

            Setup();

            gravity = 1f;
            UpdateGravity();
        }
        private void OnEnable() => roomBehaviour.SetRoomStateEvent += OnRoomStateChanged;
        private void OnDisable() => roomBehaviour.SetRoomStateEvent -= OnRoomStateChanged;
        public void UpdateGravity()
        {
            PhysicsBehaviour[] physicsBehavioursInRoom = GetComponentsInChildren<PhysicsBehaviour>();
            physicsBehavioursInRoom.Each<PhysicsBehaviour>(p => p.UpdateGravity(gravity, RoomCenter));
        }

        private void OnRoomStateChanged(bool isActive)
        {
            edgeCollider.enabled = isActive;
        }

        private void Setup()
        {
            RoomCenter = CalculateRoomCenter(RoomBoundaries);
            edgeCollider = AddEdgeCollider(RoomBoundaries);

            if (lineRenderer != null)
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

        private EdgeCollider2D AddEdgeCollider(List<Vector2> roomBoundaries)
        {
            roomBoundaries.Add(roomBoundaries[0]);
            EdgeCollider2D edgeCollider = gameObject.AddChildWithComponent<EdgeCollider2D>("Room_EdgeCollider");
            edgeCollider.SetPoints(roomBoundaries);
            edgeCollider.gameObject.layer = LayerMask.NameToLayer("Room");
            return edgeCollider;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(RoomCenter, 0.5f);
        }
    }
}
