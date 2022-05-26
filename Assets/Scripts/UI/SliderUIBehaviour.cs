using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderUIBehaviour : GroupUIBehaviour
{
    public Slider.SliderEvent OnValueChanged => slider.onValueChanged;

    Slider slider;
    protected override void Awake()
    {
        base.Awake();
        slider = GetComponent<Slider>();
    }
}
