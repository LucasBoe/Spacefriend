using SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepSoundPlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Sound neutralWalk, ladder;

    PlayerStates states;
   
    private void Start()
    {
        states = ServiceProvider.Player.GetPlayerStates();
    }
    public void Step()
    {
        if (states.WalkState.IsActive && ServiceProvider.Player.IsMoving())
            neutralWalk.Play();
    }

    public void Ladder()
    {
        if (states.LadderState.IsActive && ServiceProvider.Player.IsMoving())
            ladder.Play();
    }
}
