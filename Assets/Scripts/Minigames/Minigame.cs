using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    public MinigamePhase[] Phases;
    public void StartMinigame()
    {
        Phases.First().StartPhase();
        Phases.Last().EndPhaseEvent += EndMinigame;
    }

    public void EndMinigame()
    {

    }
}
