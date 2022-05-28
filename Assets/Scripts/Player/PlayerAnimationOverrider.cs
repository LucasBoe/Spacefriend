using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationOverrider : MonoBehaviour
{
    [SerializeField, PlayerAnimatorParam] string parameter;
    public string Parameter => parameter;

    public static System.Action<PlayerAnimationOverrider> AddOverrideEvent;
    public static System.Action<PlayerAnimationOverrider> RemoveOverrideEvent;
    private void OnEnable()
    {
        AddOverrideEvent?.Invoke(this);
    }

    private void OnDisable()
    {
        RemoveOverrideEvent?.Invoke(this);
    }
}
