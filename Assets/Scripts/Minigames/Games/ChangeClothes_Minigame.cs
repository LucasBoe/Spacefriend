using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes_Minigame : MinigamePhase
{
    [SerializeField] SliderUIBehaviour slider;
    float startValue = 0f, targetValue = 1f;
    [SerializeField] PlayerSkinType toChangeTo;
    public override void StartPhase()
    {
        base.StartPhase();
        slider.SetValue(startValue);
        slider.OnValueChanged.AddListener(OnMoveSlider);
    }

    public override void EndPhase()
    {
        base.EndPhase();
        PlayerManager.SetPlayerSkin(toChangeTo);
        slider.OnValueChanged.RemoveAllListeners();
    }

    private void OnMoveSlider(float value)
    {
        if (value == targetValue)
            EndPhase();
    }
}
