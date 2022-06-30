using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPanel_Minigame : MinigamePhase
{
    [SerializeField] SliderUIBehaviour slider;
    [SerializeField] AnimatedPanel panel;
    [SerializeField] float targetValue;
    [SerializeField] float panelCloseDelay;
    [SerializeField] float panelOpenDelay;

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
        if (IsRunning && Mathf.Abs(value - targetValue) < 0.05f)
            EndPhase();
    }

    public override void StartPhase()
    {
        base.StartPhase();
        if (panelOpenDelay > 0)
        {
            CoroutineUtil.Delay(() => panel.Open(), this, panelOpenDelay);
        }
        else
        {
            panel.Open();
        }
    }

    public override void EndPhase()
    {
        base.EndPhase();
        if (panelCloseDelay > 0)
        {
            CoroutineUtil.Delay(() => panel.Close(), this, panelCloseDelay);
        }
        else
        {
            panel.Close();
        }
    }
}
