using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public static System.Action<Interactable> BeginHoverInteractableEvent, EndHoverInteractableEvent, InteractEvent;

    [Foldout("Settings")] public string DisplayName;

    [Foldout("Settings"), SerializeField] bool useCustomWalkTargetPoint = false;
    [Foldout("Settings"), SerializeField, ShowIf("useCustomWalkTargetPoint")] Transform customWalkTargetTransform;

    [Foldout("Settings"), SerializeField] bool isConditioned = false;
    [Foldout("Settings"), SerializeField, ShowIf("isConditioned")] InteractableConditionBase[] conditions;

    private IInteractableInteractionListener[] interactionListeners;
    private IInteractableHoverListener[] hoverListeners;
    private Room room;

    private void Awake()
    {
        interactionListeners = GetComponentsInChildren<IInteractableInteractionListener>();
        hoverListeners = GetComponentsInChildren<IInteractableHoverListener>();
        room = GetComponentInParent<Room>();
    }

    internal void BeginHover()
    {
        if (!CheckAllConditions()) return;

        foreach (IInteractableHoverListener listener in hoverListeners) listener.BeginHover();
        BeginHoverInteractableEvent?.Invoke(this);
    }

    internal void EndHover()
    {
        if (!CheckAllConditions()) return;

        foreach (IInteractableHoverListener listener in hoverListeners) listener.EndHover();
        EndHoverInteractableEvent?.Invoke(this);
    }

    internal Vector3 GetPoint() => (useCustomWalkTargetPoint ? customWalkTargetTransform : transform).position;
    internal void Interact()
    {
        if (!CheckAllConditions()) return;

        EndHover();

        foreach (IInteractableInteractionListener listener in interactionListeners) listener.Interact();
        InteractEvent?.Invoke(this);
    }

    public bool CheckAllConditions()
    {
        if (!room.IsActive)
            return false;

        if (isConditioned)
        {
            foreach (InteractableConditionBase condition in conditions)
            {
                if (!condition.IsMet())
                    return false;
            }
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (customWalkTargetTransform != null)
        {
            Gizmos.DrawIcon(customWalkTargetTransform.position, "Assets/Sprites/Editor/interactable-target-icon-editor.png", true);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, customWalkTargetTransform.position);
        }
    }
}

public interface IInteractableListenerBase
{
#if UNITY_EDITOR
    string GetComponentName();
    void DrawInspector();
    void RemoveComponent();
    void SetVisible(bool visible);
    bool GetVisible();
#endif
}

public interface IInteractableHoverListener : IInteractableListenerBase
{
    void BeginHover();
    void EndHover();
}

public interface IInteractableInteractionListener : IInteractableListenerBase
{
    void Interact();
}
