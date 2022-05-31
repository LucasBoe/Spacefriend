using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMinigameActive_InteractableCondition : InteractableConditionBase
{
    [SerializeField] Minigame minigame;
    [SerializeField] bool invert;

    public override bool IsMet()
    {
        return minigame.IsRunning != invert;
    }
}
