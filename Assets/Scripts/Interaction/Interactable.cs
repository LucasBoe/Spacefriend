using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public static System.Action<Interactable> BeginHoverInteractableEvent, EndHoverInteractableEvent, InteractEvent;
    public string DisplayName;

    private IInteractionListener[] interactionListeners;
    private IInteractableHoverListener[] hoverListeners;
    private void Awake()
    {
        interactionListeners = GetComponents<IInteractionListener>();
        hoverListeners = GetComponents<IInteractableHoverListener>();
    }

    internal void BeginHover()
    {
        foreach (IInteractableHoverListener listener in hoverListeners) listener.BeginHover();
        BeginHoverInteractableEvent?.Invoke(this);
    }

    internal void EndHover()
    {
        foreach (IInteractableHoverListener listener in hoverListeners) listener.EndHover();
        EndHoverInteractableEvent?.Invoke(this);
    }

    internal Vector3 GetPoint() => transform.position;
    internal void Interact()
    {
        foreach (IInteractionListener listener in interactionListeners) listener.Interact();
        InteractEvent?.Invoke(this);
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
