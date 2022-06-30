using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinigamePhase))]
public class MinigamePhaseSoundTrigger : MonoBehaviour
{
    [SerializeField] MinigamePhase minigamePhase;
    [SerializeField] TriggerSituationType sitiation;
    [SerializeField] ActionType action;
    [SerializeField] SoundSystem.Sound sound;

    private void OnValidate()
    {
        if (minigamePhase == null)
            minigamePhase = GetComponent<MinigamePhase>();
    }

    private void OnEnable()
    {
        if (sitiation == TriggerSituationType.Start)
            minigamePhase.StartPhaseEvent += Trigger;
        else if (sitiation == TriggerSituationType.End)
            minigamePhase.EndPhaseEvent += Trigger;
    }

    private void OnDisable()
    {
        if (sitiation == TriggerSituationType.Start)
            minigamePhase.StartPhaseEvent -= Trigger;
        else if (sitiation == TriggerSituationType.End)
            minigamePhase.EndPhaseEvent -= Trigger;
    }

    [Button]
    private void Trigger()
    {
        Debug.Log(gameObject.name + " got triggered on " + sitiation);

        switch (action)
        {
            case ActionType.Play:
                sound.Play();
                return;

            case ActionType.PlayLoop:
                sound.PlayLoop();
                return;
        }
        sound.Stop();
    }

    private enum TriggerSituationType
    {
        Start,
        End,
    }

    private enum ActionType
    {
        Play,
        PlayLoop,
        Stop,
    }
}
