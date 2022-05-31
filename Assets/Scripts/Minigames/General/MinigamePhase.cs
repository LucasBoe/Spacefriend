using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePhase : MonoBehaviour
{
    UIBehaviour[] uiElements;

    public System.Action StartPhaseEvent, EndPhaseEvent;

    private bool isRunning = false;

    protected virtual void Awake()
    {
        uiElements = GetComponentsInChildren<UIBehaviour>();
    }
    public virtual void StartPhase()
    {
        isRunning = true;

        StartPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(true);
    }

    public virtual void EndPhase()
    {
        if (!isRunning) return;

        EndPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(false);
    }
}
