using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceMover : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] ParticleSystem spaceParticles;
    [SerializeField] float forceMultiplier;
    Vector3 target;
    internal void MoveTo(Vector3 point, Action callback)
    {
        target = point;
    }

    internal void Move()
    {
        rigidbody.AddForce((target - transform.position).normalized * forceMultiplier);
    }

    internal void SetSpaceMode(bool isInspaceMode)
    {
        rigidbody.bodyType = isInspaceMode ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        if (isInspaceMode) target = transform.position;
        spaceParticles.gameObject.SetActive(true);
    }
}
