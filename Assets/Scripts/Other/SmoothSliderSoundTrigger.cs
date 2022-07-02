using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothSliderSoundTrigger : MonoBehaviour
{
    [SerializeField] SmoothSliderUIBehaviour smoothSlider;
    [SerializeField] SoundSystem.Sound loop, start, end;

    private void OnEnable()
    {
        smoothSlider.ChangedMovingEvent += OnChangedMoving;
    }

    private void OnDisable()
    {
        smoothSlider.ChangedMovingEvent -= OnChangedMoving;
    }

    private void OnChangedMoving(bool moving)
    {
        if (loop != null)
        {
            if (moving)
                loop.PlayLoop();
            else
                loop.StopLoop();
        }

        if (start != null && moving)
            start.Play();

        if (end != null && !moving)
            end.Play();
    }
}
