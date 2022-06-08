using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideSoundPlayer : MonoBehaviour
{
    [SerializeField] NavigationAgent playerNavigationAgent;
    [SerializeField] Sound slidingSound;

    private void OnEnable()
    {
        playerNavigationAgent.ChangeIsSlidingEvent += OnChangeIsSliding;
    }
    private void OnDisable()
    {
        playerNavigationAgent.ChangeIsSlidingEvent -= OnChangeIsSliding;
    }

    private void OnChangeIsSliding(bool isSliding)
    {
        if (isSliding)
            slidingSound.PlayLoop();
        else
            slidingSound.StopLoop();
    }
}
