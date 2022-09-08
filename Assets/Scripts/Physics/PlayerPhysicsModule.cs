using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics.Player
{
    public class PlayerPhysicsModule : PhysicsBehaviour
    {
        PlayerStates states = null;
        public PlayerStates States => states;
        [ShowNativeProperty] string stateInfo => states == null ? "<null>" : states.CurrentState?.ToString().Split('.').Last();

        [SerializeField] PlayerPhysicsValues values = new PlayerPhysicsValues();
        [Foldout("References")]
        [SerializeField] Transform feetTransform;

        private void Awake()
        {
            states = new PlayerStates(Rigidbody);
        }

        private void FixedUpdate()
        {
            values.DistanceToGround = CalculateDistanceToGround();
            states.FixedUpdate(values);
        }

        internal override void UpdateGravity(float gravity, Vector2 roomCenter)
        {
            base.UpdateGravity(gravity, roomCenter);
            values.Gravity = gravity;
        }

        //TODO: Add overrides
        internal void SetPositionOverride(PlayerPositionOverrider playerPositionOverrider)
        {
            throw new NotImplementedException();
        }

        internal void RevokeOverridePosition(PlayerPositionOverrider playerPositionOverrider)
        {
            throw new NotImplementedException();
        }

        private float CalculateDistanceToGround()
        {
            float gravityDir = Rigidbody.gravityScale.Sign();

            RaycastHit2D hit = Physics2D.Raycast(feetTransform.position, new Vector2(0, -gravityDir), float.MaxValue, LayerMask.GetMask("Room"));
            if (hit.collider != null)
            {
                Debug.DrawLine(feetTransform.position, hit.point, Color.green);

                return Vector2.Distance(feetTransform.position, hit.point);
            }
            else
            {

                Debug.DrawRay(feetTransform.position, new Vector2(0, -gravityDir * float.MaxValue), Color.red);
            }

            return float.MaxValue;
        }

        internal void TeleportTo(Vector3 position)
        {
            Rigidbody.MovePosition(position);
        }

        internal Vector2 GetDirectionalMoveVector()
        {
            if (Rigidbody.velocity.magnitude < 0.5f) return Vector2.zero;
            return Rigidbody.velocity.normalized;
        }
    }

    [System.Serializable]
    public class PlayerPhysicsValues
    {
        [Header("Balancing")]
        [SerializeField] internal float FloatForce = 10f;
        [SerializeField] internal AnimationCurve DistanceToRotationLerpValue, DistanceToForceSupportStrength;
        [SerializeField] internal float WalkTargetVelocity = 3;
        [SerializeField] internal float WalkAccelration = 0.25f;
        [SerializeField] internal float WalkDecelleration = 0.8f;

        [Header("Debug")]
        [SerializeField, ReadOnly] internal float DistanceToGround;
        [SerializeField, ReadOnly] internal float Gravity;

    }
}
