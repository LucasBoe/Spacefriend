using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sprouts.Physics
{
    public class PhysicsBehaviour : MonoBehaviour
    {
        [Foldout("References")] [SerializeField] protected Rigidbody2D Rigidbody;
        private const float GRAVITY_UPDATE_ADDITIONAL_FORCE_MULTIPLIER = 0.05f;

        private void OnValidate()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        internal virtual void UpdateGravity(float gravity, Vector2 roomCenter)
        {
            Rigidbody.gravityScale = gravity;

            float distance = roomCenter.y - transform.position.y;
            float yForce = Mathf.Pow(distance, 2f) * GRAVITY_UPDATE_ADDITIONAL_FORCE_MULTIPLIER * Mathf.Sign(distance);

            Rigidbody.velocity += new Vector2(0, yForce);
            Rigidbody.AddTorque(Rand.om(-yForce, yForce));
        }
    }
}
