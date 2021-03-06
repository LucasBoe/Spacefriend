using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public MinigamePhase[] Phases;
    [SerializeField] private bool hasAnimations = false;
    [SerializeField, ShowIf("hasAnimations")] Animator animator;
    [SerializeField, ShowIf("hasAnimations")] AnimationClip inAnimation, outAnimation;
    [SerializeField] bool doDelayBetweenPhases = false;
    [SerializeField, ShowIf("doDelayBetweenPhases")] float delayBetweenPhases = 0f;

    private bool running = false;
    private int phaseIndex = 0;
    public bool IsRunning => running;

    [Button]
    public void StartMinigame()
    {
        foreach (MinigamePhase phase in Phases) phase.EndPhaseEvent += NextPhase;

        running = true;
        phaseIndex = 0;

        StartCoroutine(MinigameRoutine());
    }

    [Button]
    public void EndMinigame()
    {
        foreach (MinigamePhase phase in Phases) phase.EndPhaseEvent -= NextPhase;

        if (phaseIndex < Phases.Length - 1) Phases[phaseIndex].EndPhase();

        running = false;
    }

    private IEnumerator MinigameRoutine()
    {
        if (hasAnimations && inAnimation != null)
        {
            animator.Play(inAnimation.name);
            yield return new WaitForSeconds(inAnimation.length);
        }

        Phases.First().StartPhase();

        while (running) yield return null;

        if (hasAnimations && outAnimation != null)
        {
            animator.Play(outAnimation.name);
            yield return new WaitForSeconds(outAnimation.length);
        }
    }

    private void NextPhase()
    {
        phaseIndex++;
        if (phaseIndex > Phases.Length - 1)
        {
            EndMinigame();
        }
        else
        {
            MinigamePhase phase = Phases[phaseIndex];

            if (doDelayBetweenPhases)
                CoroutineUtil.Delay(phase.StartPhase, this, delayBetweenPhases);
            else
                phase.StartPhase();
        }
    }
}
