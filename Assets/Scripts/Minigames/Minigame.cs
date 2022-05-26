using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public MinigamePhase[] Phases;
    [SerializeField] private bool hasAnimations = false;
    [SerializeField, ShowIf("hasAnimations")] Animator animator;
    [SerializeField, ShowIf("hasAnimations")] AnimationClip inAnimation, outAnimation;
    private bool done = false;
    public void StartMinigame()
    {
        done = false;
        StartCoroutine(MinigameRoutine());
    }

    public void EndMinigame()
    {
        done = true;
    }

    private IEnumerator MinigameRoutine ()
    {
        if (hasAnimations && inAnimation != null)
        {
            animator.Play(inAnimation.name);
            yield return new WaitForSeconds(inAnimation.length);
        }

        Phases.First().StartPhase();
        Phases.Last().EndPhaseEvent += EndMinigame;

        while (!done) yield return null;

        if (hasAnimations && outAnimation != null)
        {
            animator.Play(outAnimation.name);
            yield return new WaitForSeconds(outAnimation.length);
        }
    }
}
