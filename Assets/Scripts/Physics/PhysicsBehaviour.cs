using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics
{
    public class PhysicsBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody;
        private const float GRAVITY_UPDATE_ADDITIONAL_FORCE_MULTIPLIER = 0.05f;

        internal void UpdateGravity(float gravity, Vector2 roomCenter)
        {
            rigidbody.gravityScale = gravity;

            float distance = roomCenter.y - transform.position.y;
            float yForce = Mathf.Pow(distance, 2f) * GRAVITY_UPDATE_ADDITIONAL_FORCE_MULTIPLIER * Mathf.Sign(distance);

            rigidbody.velocity += new Vector2(0, yForce);
            rigidbody.AddTorque(Rand.om(-yForce, yForce));
        }
    }
}
