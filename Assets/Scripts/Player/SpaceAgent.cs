using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceAgent : SpaceRoomObject
{
    [SerializeField] ParticleSystem spaceParticles;
    [SerializeField] float forceMultiplier;
    Vector3 target;
    internal void MoveTo(Vector3 point, Action callback)
    {
        target = point;
    }

    internal void Move()
    {
        Rigidbody.AddForce((target - transform.position).normalized * forceMultiplier);
    }

    public override void SetSpaceMode(bool isInspaceMode)
    {
        base.SetSpaceMode(isInspaceMode);
        Rigidbody.bodyType = isInspaceMode ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        if (isInspaceMode) target = transform.position;
        spaceParticles.gameObject.SetActive(true);
    }
}
