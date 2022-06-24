using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog_InteractionListener : MonoBehaviour, IInteractionListener
{
    public void Interact()
    {
        Debug.Log(name);
    }

#if UNITY_EDITOR
    public string GetComponentName() => "Debug Log";
    public void DrawInspector() { }
#endif
}
