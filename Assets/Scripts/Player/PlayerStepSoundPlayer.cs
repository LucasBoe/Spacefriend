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
        states = PlayerServiceProvider.GetPlayerStates();
    }
    public void Step()
    {
        if (states.WalkState.IsActive && PlayerServiceProvider.Info.IsMoving())
            neutralWalk.Play();
    }

    public void Ladder()
    {
        if (states.LadderState.IsActive && PlayerServiceProvider.Info.IsMoving())
            ladder.Play();
    }
}
