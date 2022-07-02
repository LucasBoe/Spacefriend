using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigamePhaseTriggerBase : MonoBehaviour
{
    [SerializeField] MinigamePhase minigamePhase;
    [SerializeField] protected TriggerSituationType situation;

    private void OnValidate()
    {
        if (minigamePhase == null)
            minigamePhase = GetComponent<MinigamePhase>();
    }

    private void OnEnable()
    {
        if (situation == TriggerSituationType.Start)
            minigamePhase.StartPhaseEvent += Trigger;
        else if (situation == TriggerSituationType.End)
            minigamePhase.EndPhaseEvent += Trigger;
    }

    private void OnDisable()
    {
        if (situation == TriggerSituationType.Start)
            minigamePhase.StartPhaseEvent -= Trigger;
        else if (situation == TriggerSituationType.End)
            minigamePhase.EndPhaseEvent -= Trigger;
    }

    protected abstract void Trigger();

    protected enum TriggerSituationType
    {
        Start,
        End,
    }
}
