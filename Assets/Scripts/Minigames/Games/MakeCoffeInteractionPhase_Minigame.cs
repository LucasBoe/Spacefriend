using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoffeInteractionPhase_Minigame : MinigamePhase
{
    [SerializeField] ButtonUIBehaviour button;
    public override void StartPhase()
    {
        base.StartPhase();
        button.OnClick.AddListener(EndPhase);
    }

    public override void EndPhase()
    {
        base.EndPhase();
        button.OnClick.RemoveListener(EndPhase);
    }
}
