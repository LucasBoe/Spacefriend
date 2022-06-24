using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableInteraction_BaseBehaviour : MonoBehaviour, IInteractableInteractionListener
{
    public abstract void Interact();

#if UNITY_EDITOR
    public virtual string GetComponentName() => GetType().ToString().Split('_')[0];
    public virtual void DrawInspector() { }
    public void RemoveComponent() => DestroyImmediate(this);
    public void SetVisible(bool visible) => hideFlags = visible ? HideFlags.None : HideFlags.HideInInspector;

    public bool GetVisible() => hideFlags == HideFlags.None;

#endif
}
