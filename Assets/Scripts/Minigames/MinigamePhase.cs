using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePhase : MonoBehaviour
{
    UIBehaviour[] uiElements;

    public System.Action StartPhaseEvent, EndPhaseEvent;

    private void Awake()
    {
        uiElements = GetComponentsInChildren<UIBehaviour>();
    }
    public void StartPhase()
    {
        StartPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(true);
    }

    public void EndPhase()
    {
        EndPhaseEvent?.Invoke();
        foreach (UIBehaviour behaviour in uiElements) behaviour.SetActive(false);
    }
}
