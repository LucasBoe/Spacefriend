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

    private bool running = false;
    private int phaseIndex = 0;
    public bool IsRunning => running;
    public void StartMinigame()
    {
        running = true;
        phaseIndex = 0;
        StartCoroutine(MinigameRoutine());
    }

    public void EndMinigame()
    {
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
        foreach (MinigamePhase phase in Phases) phase.EndPhaseEvent += NextPhase;

        while (running) yield return null;

        foreach (MinigamePhase phase in Phases) phase.EndPhaseEvent -= NextPhase;

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
            Phases[phaseIndex].StartPhase();
        }
    }
}
