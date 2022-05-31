using SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoffeProductionPhase_Minigame : MinigamePhase
{
    [SerializeField] ParticleSystem coffeeDownParticles, coffeeOverflowPartices;
    [SerializeField] Sound coffeeProductionSound;
    [SerializeField] MakeCoffe_MugManager mugVisualizer;
    [SerializeField] GameObject finishedMug;
    [SerializeField] CoffeMugItemData mugItem;
    public override void StartPhase()
    {
        base.StartPhase();
        coffeeProductionSound.Play();
        CoroutineUtil.Delay(() =>
        {
            coffeeDownParticles.Play();
            if (!mugVisualizer.MugActive) coffeeOverflowPartices.Play();
        }, this, 2);

        CoroutineUtil.Delay(() => EndPhase(), this, 10f);
    }

    public override void EndPhase()
    {
        base.EndPhase();
        if (mugVisualizer.MugActive)
        {
            finishedMug.SetActive(true);
            mugVisualizer.MugActive = false;
            mugItem.HoldsCoffee = true;
        }
    }
}
