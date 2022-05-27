using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that reacts to zero g and turns physically active in space
/// </summary>
public class SpaceRoomObject : MonoBehaviour
{
    public System.Action<bool> ChangeSpaceModeEvent;
    [SerializeField] protected Rigidbody2D Rigidbody;

    public virtual void SetSpaceMode(bool isSpaceMode)
    {
        StartCoroutine(SpaceModeTransition(isSpaceMode));
    }

    private IEnumerator SpaceModeTransition(bool isSpaceMode)
    {
        if (isSpaceMode)
        {
            ChangeSpaceModeEvent?.Invoke(true);
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        float t = 0f;
        float duration = 2f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            yield return null;

            Rigidbody.gravityScale = isSpaceMode ? 1f - t : t;
        }

        if (!isSpaceMode)
        {
            ChangeSpaceModeEvent?.Invoke(false);
            Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
