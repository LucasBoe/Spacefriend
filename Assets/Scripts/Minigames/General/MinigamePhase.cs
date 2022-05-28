using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePhase : MonoBehaviour
{
    UIBehaviour[] uiElements;

    public System.Action StartPhaseEvent, EndPhaseEvent;

    protected virtual void Awake()
    {
        uiElements = GetComponentsInChildren<UIBehaviour>();
    }
    public virtual void StartPhase()
    {
        StartPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(true);
    }

    public virtual void EndPhase()
    {
        EndPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(false);
    }
}
