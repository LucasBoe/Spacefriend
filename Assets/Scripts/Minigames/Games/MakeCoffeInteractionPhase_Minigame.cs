using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoffeInteractionPhase_Minigame : MinigamePhase
{
    [SerializeField] ButtonUIBehaviour button;
    [SerializeField] MakeCoffe_MugManager mug;
    [SerializeField] ItemData mugItem;
    public override void StartPhase()
    {
        base.StartPhase();
        button.OnClick.AddListener(EndPhase);
    }

    private void Update()
    {
        if (!IsRunning || mug.MugActive) return;

        if (PlayerServiceProvider.GetPlayerItemInHand() == mugItem)
        {
            PlayerServiceProvider.RemoveItemFromHand(transform);
            mug.MugActive = true;
        }    
    }

    public override void EndPhase()
    {
        base.EndPhase();
        button.OnClick.RemoveListener(EndPhase);
    }
}
