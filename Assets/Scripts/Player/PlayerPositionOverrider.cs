using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOverrider : MonoBehaviour
{
    public static System.Action<PlayerPositionOverrider> AddOverrideEvent;
    public static System.Action<PlayerPositionOverrider> RemoveOverrideEvent;
    private void OnEnable() => AddOverrideEvent?.Invoke(this);
    private void OnDisable() => RemoveOverrideEvent?.Invoke(this);
}
