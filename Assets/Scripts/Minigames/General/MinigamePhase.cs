using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePhase : MonoBehaviour
{
    UIBehaviour[] uiElements;

    public System.Action StartPhaseEvent, EndPhaseEvent;

    [SerializeField, ReadOnly] protected bool IsRunning = false;

    protected virtual void Awake()
    {
        uiElements = GetComponentsInChildren<UIBehaviour>();
    }
    public virtual void StartPhase()
    {
        IsRunning = true;

        StartPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(true);
    }

    public virtual void EndPhase()
    {
        if (!IsRunning) return;

        IsRunning = false;

        EndPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(false);
    }
}
