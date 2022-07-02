using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinigamePhase))]
public class MinigamePhaseTrigger_Sound : MinigamePhaseTriggerBase
{

    [SerializeField] ActionType action;
    [SerializeField] SoundSystem.Sound sound;

    [Button]
    protected override void Trigger()
    {
        Debug.Log(sound.name + " got triggered on " + situation);

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

    private enum ActionType
    {
        Play,
        PlayLoop,
        Stop,
    }
}
