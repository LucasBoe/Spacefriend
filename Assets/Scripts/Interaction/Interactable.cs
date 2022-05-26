using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public static System.Action<Interactable> BeginHoverInteractableEvent, EndHoverInteractableEvent, InteractEvent;
    
    [Foldout("Settings")] public string DisplayName;

    [Foldout("Settings"), SerializeField] bool useCustomWalkTargetPoint = false;
    [Foldout("Settings"), SerializeField, ShowIf("useCustomWalkTargetPoint")] Transform customWalkTargetTransform;

    [Foldout("Settings"), SerializeField] bool isConditioned = false;
    [Foldout("Settings"), SerializeField, ShowIf("isConditioned")] InteractableConditionBase[] conditions;



    private IInteractionListener[] interactionListeners;
    private IInteractableHoverListener[] hoverListeners;
    private void Awake()
    {
        interactionListeners = GetComponentsInChildren<IInteractionListener>();
        hoverListeners = GetComponentsInChildren<IInteractableHoverListener>();
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

        foreach (IInteractionListener listener in interactionListeners) listener.Interact();
        InteractEvent?.Invoke(this);
    }

    private bool CheckAllConditions()
    {
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
}

public interface IInteractableHoverListener
{
    void BeginHover();
    void EndHover();
}

public interface IInteractionListener
{
    void Interact();
}
