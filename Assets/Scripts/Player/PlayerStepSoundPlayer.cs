using SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepSoundPlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Sound neutralWalk, ladder;
    public void Step()
    {
        if (Mathf.Abs(player.MoveModule.GetDirectionalMoveVector().x) > 0.5f)
            neutralWalk.Play();
    }

    public void Ladder()
    {
        if (Mathf.Abs(player.MoveModule.GetDirectionalMoveVector().y) > 0.5f)
            ladder.Play();
    }
}
