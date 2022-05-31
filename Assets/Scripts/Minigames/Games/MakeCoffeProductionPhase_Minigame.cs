using SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoffeProductionPhase_Minigame : MinigamePhase
{
    [SerializeField] ParticleSystem coffeeDownParticles, coffeeOverflowPartices;
    [SerializeField] Sound coffeeProductionSound;
    public override void StartPhase()
    {
        base.StartPhase();
        coffeeProductionSound.Play();
        CoroutineUtil.Delay(() =>
        {
            coffeeDownParticles.Play();
            coffeeOverflowPartices.Play();
        }, this, 2);
        CoroutineUtil.Delay(() => EndPhase(), this, 10f);
    }
}
