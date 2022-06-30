using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPanel_Minigame : MinigamePhase
{
    [SerializeField] SliderUIBehaviour slider;
    [SerializeField] AnimatedPanel panel;
    [SerializeField] float targetValue;

    private void OnEnable()
    {
        slider.OnValueChanged.AddListener(OnValueChanged);
    }
    private void OnDisable()
    {
        slider.OnValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        if (IsRunning && value == targetValue)
            EndPhase();
    }

    public override void StartPhase()
    {
        base.StartPhase();
        panel.Open();
    }

    public override void EndPhase()
    {
        base.EndPhase();
        panel.Close();
    }
}
