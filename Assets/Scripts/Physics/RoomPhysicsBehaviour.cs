using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics
{
    public class RoomPhysicsBehaviour : MonoBehaviour
    {
        [Foldout("References")][SerializeField] protected Rigidbody2D Rigidbody;
        [Foldout("References")][SerializeField] private RoomPhysicsModule roomPhysics;
        private const float GRAVITY_UPDATE_ADDITIONAL_FORCE_MULTIPLIER = 0.5f;

        private void OnValidate()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            roomPhysics = GetComponentInParent<RoomPhysicsModule>();
        }

        protected virtual void OnEnable() => TrySubscribeToPhysicsModule(roomPhysics);
        protected virtual void OnDisable() => TryUnsubscribeFromPhysicsModule(roomPhysics);
        protected void ChangeRoomPhysicsModule(RoomPhysicsModule newRoomPhysics)
        {
            TryUnsubscribeFromPhysicsModule(roomPhysics);
            roomPhysics = newRoomPhysics;
            TrySubscribeToPhysicsModule(roomPhysics);
        }
        private void TryUnsubscribeFromPhysicsModule(RoomPhysicsModule roomPhysics)
        {
            if (roomPhysics != null) roomPhysics.ChangeGravityEvent -= OnChangeGravity;
        }

        private void TrySubscribeToPhysicsModule(RoomPhysicsModule roomPhysics)
        {
            if (roomPhysics != null) roomPhysics.ChangeGravityEvent += OnChangeGravity;
        }

        internal virtual void OnChangeGravity(float gravity, Vector2 roomCenter)
        {
            Rigidbody.gravityScale = gravity;

            float distance = roomCenter.y - transform.position.y;
            float yForce = Mathf.Pow(distance, 2f) * GRAVITY_UPDATE_ADDITIONAL_FORCE_MULTIPLIER * Mathf.Sign(distance);

            Rigidbody.AddForceIgnoreMass(new Vector2(0, yForce));
            Rigidbody.AddTorque(Rand.om(-yForce, yForce));
        }
    }
}
