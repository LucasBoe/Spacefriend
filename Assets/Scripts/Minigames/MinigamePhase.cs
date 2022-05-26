using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePhase : MonoBehaviour
{
    public System.Action StartPhaseEvent, EndPhaseEvent;
    public void StartPhase()
    {
        StartPhaseEvent?.Invoke();
    }

    public void EndPhase()
    {
        EndPhaseEvent?.Invoke();
    }
}
