using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Physics2DUtil
{

    public static void AddForceIgnoreMass(this Rigidbody2D rigidbody, Vector2 force)
    {
        rigidbody.AddForce(force * rigidbody.mass);
    }
}
